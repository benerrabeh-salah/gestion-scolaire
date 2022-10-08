--Base  de Donneés De projet fin d'etudes ( Application desktop Gestion D'une Ecole )--
Create database Gestion_Scolarite
go
Use Gestion_Scolarite
go
Create table Login (
LoginA Varchar (10),
Mdp Varchar (20),
SerieA Varchar (20) primary key,
NomA Varchar (30),
)
go
insert into Login Values ('admin','123','HH1234','Hamza')
--select * from Login
go
----------------------------------------TABLE Filiére ----------------------------------------------------------------------
Create  table Filiere(
Code_F int,
libelle varchar (10),
type_F Varchar (20),
Idfil int identity(1,1),
constraint PK_filiere primary key (idfil)
)
select*From Filiere

--insert into Filiére values(1,'TDI','1er Annee')
go

----------------------------------------TABLE ETUDIANT----------------------------------------------------------------------
Create Table Stagiaire(
CIN Varchar(20) primary key ,
Nom Varchar (20),
Prenom varchar (20),
CNE varchar(20) ,
Date_Naissance Date ,
Numero_telephone varchar(15),-- check (Numero_telephone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
Adresse Varchar (100),
Idfil int,
constraint FK_Etud_Filiere foreign key (idfil) references Filiere (idfil) ON DELETE CASCADE on UPDATE CASCADE,
)
--select e.Code_E,e.Nom,e.Prenom,e.CIN,e.Date_Naissance,e.Numero_telephone,e.Adresse,f.libelle from Etudiant e,Filiére f where e.Code_F = f.Code_F
go
----------------------------------------TABLE Professeur ---------------------------------------------------------------------
Create table Formateur(
Cin_F varchar(20) primary key  ,
Nom Varchar (20),
Prenom Varchar (20),
DateNP date,
Sexe varchar(10),
Adresse varchar(100),
TelP varchar(20),
)
--select*From Professeur
go
----------------------------------------TABLE Matiére ---------------------------------------------------------------------
Create table Module(
Code_M int Primary key,
libelle_M varchar (50),
coefficient int ,
Mh_M int,
Cin_F varchar(20) foreign key references Formateur (Cin_F) ON DELETE CASCADE on UPDATE CASCADE
)
--select*From Matiére
go
----------------------------------------TABLE Evaluation ---------------------------------------------------------------------
Create table Evaluation (
CIN varchar(20) foreign key references Stagiaire (CIN) ON DELETE CASCADE on UPDATE CASCADE,
Code_M int foreign key references Module (Code_M) ON DELETE CASCADE on UPDATE CASCADE,
dateEval varchar(20),
note Float,
constraint PK_Evaluation primary key (CIN,Code_m,dateEval) 
)
--select*From Evaluation
go
----------------------------------------TABLE des Absences -------------------------------------------------------------------
Create  table AbsenceS(
CIN varchar(20) foreign key references  Stagiaire (CIN) ON DELETE CASCADE on UPDATE CASCADE,
Code_M int   foreign key references Module (Code_M) ON DELETE CASCADE on UPDATE CASCADE,
date_absence varchar(20),
Nombre_Heure int ,
constraint PK_AbsenceE primary key(CIN,Code_M,date_absence)
)
drop table AbsenceP
insert into AbsenceE values('1','105','10/10/2020',4)
select * from AbsenceE
--select*From AbsenceE
go
Create table AbsenceF(
Cin_F varchar(20)  foreign key references  Formateur (Cin_F) ON DELETE CASCADE on UPDATE CASCADE,
date_absence varchar(20),
Nombre_Heure int ,
constraint PK_AbsenceP primary key(Cin_F,date_absence)
)
--select*From AbsenceP

-------Proc
create proc ProcInsert @codef int,@lib varchar(20),@typef varchar(20)
as
begin
   if EXISTS (select * from Filiere where Code_F=@codef and libelle = @lib and type_F = @typef)
      rollback;
   else
      insert into Filiere values (@codef,@lib,@typef)
end


--view
create view CrystalAffiche as
select e.CIN,e.Nom,e.Prenom,m.coefficient,m.libelle_M,sum(v.note)/(select count(x.note) 
from Module n,Evaluation x  where n.Code_M = x.Code_M and n.libelle_M = m.libelle_M and x.CIN =e.CIN) as Moyenne 
from Module m ,Stagiaire e ,evaluation v
where m.Code_M = v.Code_M and v.CIN = e.CIN 
group by e.CIN,e.Nom,e.Prenom,m.coefficient,m.libelle_M

select * from CrystalAffiche where cin ='1'
select count(x.note) from Module n,Evaluation x  where n.Code_M = x.Code_M and n.libelle_M = 'arabe' 