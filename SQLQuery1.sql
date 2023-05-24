create database LMS
use LMS
create table User_details(
Username  varchar(100),
UserPassword varchar(100)
)
insert into User_details  values('mani','maniss'),('surya','suryass')
drop table User_details

select * from User_details

drop table User_details

select * from Student_details

create table Student_details(
reg_id int identity primary key ,
name varchar(100),
email varchar(100),
Address varchar(100)
)

create table Book_details(
book_id int identity primary key,
book_name varchar(100),
author varchar(50),
Quantity int
)

select * from Book_details
drop table Book_details
drop table Student_details
drop table User_details

create table Issue_book(
book_id int ,
reg_id int,
IssueDate date,
primary key (book_id,reg_id)
)

drop table Issue_book


select * from  Book_details
select * from User_details
select * from Student_details
select * from Issue_book

