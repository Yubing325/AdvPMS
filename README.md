# AdvPMS
Self built project management system


### API endpoints developped so far (till Dec 29th.2020):

Iteration APIs	
- GET api/iterations/	
- GET api/iterations/{id}	
- POST api/iterations/	
- DELETE api/iterations/{id}	
- PUT api/iterations/{id}

WorkItem APIs	
- GET api/workItems/	
- GET api/iterations/{iterationId}/WorkItems/	
- GET api/iterations/{iterationId}/WorkItems/{workItemId}	
- POST api/iterations/{iterationId}/WorkItems/
- DELETE api/WorkItems/{workItemId}	
- PUT api/iterations/{iterationId}/WorkItems/{workItemId}	


### Structures:

Angular Modules

Sprint Module

  components
  
    story-boards    
    iterations    
      iteration-grid      
      iteration-edit      
    workItems    
      workItem-detail      
      workItem-edit      
      workItem-grid      
    toolbar
    
User Registeration & login (TBD)
project documents module (TBD)
Message Module(TBD)

Backend:

  BusinessLogic
  
    Services/
      WorkItemService
      IterationService
  
	Data	
    Entities/
    Enums/
    Interfaces/
    Migartions/
    Repositories/
	Web.Api
    Controllers/
    Dtos/
    Extensions/
    Helpers/
    
		
 

