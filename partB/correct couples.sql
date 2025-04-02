create procedure comleteCouples
as
begin
declare @Current_Id int;

declare ID_Cursor cursor for
select p2.Person_ID from relatives p1
join catalogConectionType on p1.Conection_Type_Code=Type_Code
join relatives p2 on p1.Relative_ID=p2.Person_ID
and p2.Conection_Type_Code=Type_Code
and (select Relative_ID from relatives where Person_ID=p2.Person_ID) is null
and Type_Description in ('wife','hasbend') 

open ID_Cursor
fetch next from ID_Cursor into @Current_Id

while @@FETCH_STATUS=0
begin

update Relatives
set Relative_ID=@Current_Id
where Person_ID=
(select Relative_ID from relatives where Person_ID=@Current_Id)

fetch next from ID_Cursor into @Current_Id
end
end

exec comleteCouples
