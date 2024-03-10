create database dbCadastroPessoa

use dbCadastroPessoa


create table tbPessoa(
	codigo int identity(1,1) not null,
	nome varchar(20) not null,
	sobrenome  varchar(30) not null,
	dataNasc date not null,
	estadoCivil varchar(15) not null,
	cpf varchar(14) not null,
	rg varchar(12) not null
	constraint PK_Pessoa primary key(codigo)
)
