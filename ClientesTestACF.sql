

-- nombre del server local para Mi
-- DESKTOP-3MKH7IC\SQLEXPRESS 

create database ClientesTestACF

use ClientesTestACF


create table Clientes(
Identificación int identity(1,1),
primerNombre varchar(15) not null default '',
primerApellido varchar(15) not null default '',
edad int not null,
fechaDeCreación DateTime default GetDate()
)

create table Usuarios(
idUsuario int identity(1,1), 
nombreUsuario varchar(50) not null,
contraseña varchar(100) not null,
tipoUsuario int not null
)

insert into Usuarios(nombreUsuario,contraseña,tipoUsuario) values('admin','123456',1)



insert into Clientes(primerNombre,primerApellido,edad) values('Wilber','Ramirez',26)


select * from Clientes


select * from Usuarios

