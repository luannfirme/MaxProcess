## MassProcess ##

Para executar a aplicação via docker.<br>
Utilize o commando:
 ```
   docker-compose up --build
 ```
<br>

Antes de iniciar o projeto, é necessário aplicar as migrations.<br>
Utilize o commando:
 ```
   dotnet ef database update --project MaxProcess.Persistence --startup-project MaxProcess.API
 ```
<br>
* Caso queira cadastrar um usuário primeiramente, commente a linha _[Authorize]_ em UsuariosController.
