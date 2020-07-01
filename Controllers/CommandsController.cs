using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.DTOS;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    //api/[controller]  --> incase of make it not hard coded
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository,IMapper mapper)
        {   
            _repository=repository;    
            _mapper= mapper;    
        }
        
        //GET api/command
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDTO>> GetAllCommands() {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }
        //GET api/command/{id}
        
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommandReadDTO> GetCommandById(int id){
            var commandItem=_repository.GetCommandById(id);
            if (commandItem!=null){
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));
            }else{
                return NotFound();
            }
        }

        //POST api/command
        [HttpPost]
        public ActionResult <CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            
            var commandModel=_mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);
            
            return CreatedAtRoute(nameof(GetCommandById),new {Id=commandReadDTO.Id},commandReadDTO);
            //return Ok(commandReadDTO);

       }

       //PUT api/command/{id}
       [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id,CommandUpdateDTO commandUpdateDTO)
        {
            var commandModelFromRepo=_repository.GetCommandById(id);
            if(commandModelFromRepo==null){
                return NotFound(); 
            }
            _mapper.Map(commandUpdateDTO,commandModelFromRepo);
            
            _repository.UpdateCommand(commandModelFromRepo);
            
            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/command/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDTO> patchDoc)
        {
            var commandModelFromRepo=_repository.GetCommandById(id);
            if(commandModelFromRepo==null){
                return NotFound(); 
            }
            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch,ModelState);
            if(!TryValidateModel(commandToPatch)){
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch,commandModelFromRepo);

            _repository.UpdateCommand( commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/command/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo=_repository.GetCommandById(id);
            if(commandModelFromRepo==null){
                return NotFound(); 
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}