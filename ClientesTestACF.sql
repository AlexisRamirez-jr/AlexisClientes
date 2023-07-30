
create database ClientesTestACF

use ClientesTestACF


create table Clientes(
Identificación int identity(1,1),
primerNombre varchar(15) not null default '',
primerApellido varchar(15) not null default '',
edad int not null,
fechaDeCreación DateTime default GetDate()
)

select * from Clientes

insert into Clientes(primerNombre,primerApellido,edad) values('Wilber','Ramirez',26)