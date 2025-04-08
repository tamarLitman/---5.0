alter procedure comleteCouples
as
begin
declare @Current_Id int;

declare relative_Cursor cursor for
select p1.Person_ID from relatives p1
join relatives p2 on p1.Relative_ID=p2.Person_ID
and p1.Conection_Type_Code in(7,8)
and p2.Conection_Type_Code in(7,8)
and (select Relative_ID from relatives where Person_ID=p2.Person_ID) is null

open relative_Cursor
fetch next from relative_Cursor into @Current_Id

while @@FETCH_STATUS=0
begin

update Relatives
set Relative_ID=@Current_Id
where Person_ID=
(select Relative_ID from relatives where Person_ID=@Current_Id)

fetch next from relative_Cursor into @Current_Id
end
close relative_Cursor
deallocate relative_Cursor

declare Person_Cursor cursor for
select p1.Person_ID from Person p1
join Person p2 on p1.spouse_Id=p2.Person_ID
and (select spouse_Id from person where Person_ID=p2.Person_ID) is null

open Person_Cursor
fetch next from Person_Cursor into @Current_Id

while @@FETCH_STATUS=0
begin

update Person
set Spouse_Id=@Current_Id
where Person_ID=
(select Spouse_Id from person where Person_ID=@Current_Id)

fetch next from Person_Cursor into @Current_Id
end
close Person_Cursor
deallocate Person_Cursor

end

exec comleteCouples
