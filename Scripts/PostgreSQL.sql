create table Results(
id serial PRIMARY KEY NOT NULL,
Result_Qual1 int,
Result_Qual2 int,
Result_Qual3 int,
Result_Qual4 int
);

create table Complexity(
id serial PRIMARY KEY NOT NULL,
Complexity_Qual1 int,
Complexity_Qual2 int,
Complexity_Qual3 int,
Complexity_Qual4 int
);

create table Qualifications(
id serial PRIMARY KEY NOT NULL,
Name varchar(30) not null,
Coefficient decimal(2,1) not null
);

create table Type(
id serial PRIMARY KEY NOT NULL,
Name varchar(20) not null
);

create table Status(
id serial PRIMARY KEY NOT NULL,
Name varchar(20) not null
);

create table Tasks(
id serial PRIMARY KEY NOT NULL,
Name varchar(200) not null,
id_ParentTask int,
Description text,
id_Complexity int not null,
Date_Delivery date not null,
id_TaskManager int not null,
FOREIGN KEY (id_ParentTask) REFERENCES Tasks (id) ON DELETE CASCADE,
FOREIGN KEY (id_Complexity) REFERENCES Complexity (id) ON DELETE CASCADE
);

create table Employees(
id serial PRIMARY KEY NOT NULL,
FIO varchar(60) not null,
DateOfBirth date not null,
id_Qualification int not null,
Login varchar(20) not null unique,
Password varchar(20) not null unique,
id_Type int not null,
FOREIGN KEY (id_Qualification) REFERENCES Qualifications (id) ON DELETE CASCADE,
FOREIGN KEY (id_Type) REFERENCES Type (id) ON DELETE CASCADE
);


create table AssignedTasks(
id serial PRIMARY KEY NOT NULL,
id_Task int not null,
id_Employee int not null,
Date_Start date not null,
Date_End date,
id_Result int,
Comment text,
FOREIGN KEY (id_Task) REFERENCES Tasks (id) ON DELETE CASCADE, 
FOREIGN KEY (id_Employee) REFERENCES Employees (id) ON DELETE CASCADE,
FOREIGN KEY (id_Result) REFERENCES Results (id) ON DELETE CASCADE
);

create table EventLog(
id serial PRIMARY KEY NOT NULL,
Date timestamp not null,
id_LastStatus int,
id_CurrentStatus int,
id_Employee int not null,
id_Task int not null,
FOREIGN KEY (id_LastStatus) REFERENCES Status (id) ON DELETE CASCADE,
FOREIGN KEY (id_CurrentStatus) REFERENCES Status (id) ON DELETE CASCADE,
FOREIGN KEY (id_Employee) REFERENCES Employees (id) ON DELETE CASCADE,
FOREIGN KEY (id_Task) REFERENCES Tasks (id) ON DELETE CASCADE
);

insert into Qualifications(Name, Coefficient) values('Инженер 3-категории', 1.0),('Инженер 2-категории', 1.2),('Инженер 1-категории', 1.4),('Главный инженер', 1.5);

insert into Status(Name) values('Создан'),('Назначен'),('На выполнении'),('Приостановлен'),('Завершен');

insert into Complexity(Complexity_Qual1,Complexity_Qual2,Complexity_Qual3,Complexity_Qual4) values(0, 10, 10, 0),(30, 20, 0, 0),(22, 25, 10, 0),(0, 0, 0, 35),(60,0,0,0),(20,0,15,0);

insert into Type(Name) values('Admin'),('Director'),('User');

insert into Employees(FIO, DateOfBirth, id_Qualification, Login, Password, id_Type) values ('Дынин Николай Вадимович', '16-01-1996', 4, 'Admin','Admin', 1), ('Алексеев Дмитрий Андреевич', '24-09-1996', 3, 'abdc50','123456',2), ('Свечников Егор Александрович', '15-06-1995', 1, 'user','user',3);

insert into Tasks(Name, id_Complexity, Date_Delivery, id_TaskManager) values('Automated information management systems', 1, '15-06-2019', 1),('Purpose of the adaptive matrix multiplier', 2, '31-05-2019', 1),('Adequacy and objectivity of information management system modeling', 3, '16-05-2019', 1),
('The monitoring algorithm of the network status data', 4, '19-05-2019', 1),('The essence and features of the algorithm of the input block and data processing', 5, '26-05-2019', 1), ('Evaluation of traffic', 6, '05-06-2019', 1);


WITH RECURSIVE r AS (
   SELECT id, Task_name, id_ParentTask
   FROM Tasks
   WHERE id_ParentTask = 1

   UNION

   SELECT Tasks.id, Tasks.Task_name, Tasks.id_ParentTask 
   FROM Tasks
      JOIN r
          ON Tasks.id_ParentTask = r.id
)

SELECT * FROM r;