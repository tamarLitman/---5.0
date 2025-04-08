create table Person
(
	Person_Id int primary key,
	Personal_Name varchar(10),
	Family_Name varchar(10),
	Gender int,
	Father_Id int,
	Mother_Id int,
	Spouse_Id int
	foreign key (Gender) references GenderType(Gender_Code)
);

create table GenderType
(
	Gender_Code int primary key,
	Gender_Description varchar
)


insert into GenderType
(
	Gender_Code,
	Gender_Description
)
values
(1,'male'),
(2,'female')

alter table GenderType 
alter column Gender_Description varchar(10)

create table Relatives
(
	Person_ID int ,
	Relative_ID int ,
	Conection_Type_Code int 
	foreign key (Person_ID) references Person(Person_Id),
	foreign key (Relative_ID) references Person(Person_Id)
)
alter table Relatives 
ADD CONSTRAINT Relative_ID DEFAULT null FOR Relative_ID;

ALTER TABLE Relatives 
ALTER COLUMN Conection_Type_Code INT NOT NULL;


create table catalogConectionType
(
	Type_Code int primary key,
	Type_Description varchar
)

alter table catalogConectionType 
alter column Type_Description varchar(10)

insert into catalogConectionType
(
	Type_Code,
	Type_Description
)
values
(1,'father'),
(2,'mother'),
(3,'brother'),
(4,'sister'),
(5,'son'),
(6,'daughter'),
(7,'wife'),
(8,'hasbend')

insert into Person
(
	Person_Id
    ,[Personal_Name]
    ,[Family_Name]
    ,[Gender]
    ,[Father_Id]
    ,[Mother_Id]
    ,[Spouse_Id]
)
values
(1,'A','AA',1,3,4,2),
(2,'B','BB',2,5,6,null)

insert into Relatives
(
	[Person_ID]
   ,[Relative_ID]
   ,[Conection_Type_Code]
)
values
(1,2,7),
(2,null,8)