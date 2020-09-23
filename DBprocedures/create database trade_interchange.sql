create database trade_interchange


use trade_interchange
create table department(
						department_id Integer Primary Key Not Null,
						department_name VarChar(20) Not Null
						)

create table employee (
						
						employee_id Integer Primary Key Not Null,
						employee_number Integer Not Null,
						employee_forename VarChar(20) Not Null,
						employee_surname VarChar(20) Not Null,
						dob Date Not Null,
						department_id Integer Not Null,
						foreign key (department_id) references department(department_id)
					  )


Insert Into department(department_name)
values('IT')

Insert Into department(department_name)
values('Financial')

Insert Into department(department_name)
values('Management')


--------------------------------------------------------------------------------------------------------------------------------------------------------------------

use trade_interchange

Go 
Create Procedure add_employee(@employee_number Integer, @employee_forename VarChar(20),@employee_surname VarChar (20), @dob Date, @department_id Integer)
As
Begin
Insert Into employee(employee_number, employee_forename ,employee_surname ,dob ,department_id)
values(@employee_number , @employee_forename ,@employee_surname , @dob , @department_id )
End


exec add_employee @employee_number = 101,@employee_forename = 'Ben',@employee_surname='Durm',@dob = '1997-01-09', @department_id = 2






select e.employee_number, e.employee_forename, e.employee_surname, e.dob, d.department_name 
from employee as e
Join department as d on e.department_id = d.department_id



---------------------------------------------------------------------------------------------------------------------------------------------------------------------

Create Procedure update_employee(
									@employee_id Integer,
									@employee_number Integer,
									@employee_forename VarChar(20),
									@employee_surname VarChar(20),
									@dob Date,
									@department_id Integer
								)
As
Begin
Update employee
Set employee_number = @employee_number,
	employee_forename = @employee_forename,
	employee_surname = @employee_surname,
	dob = @dob,
	department_id = @department_id
Where employee_id = @employee_id
End


----------------------------------------------------------------------------------------------------------------------------------------------------------------------


Create Procedure delete_employee (
									
									@employee_id Integer
								  )

As 
Begin
Delete from employee where employee_id = @employee_id
End





