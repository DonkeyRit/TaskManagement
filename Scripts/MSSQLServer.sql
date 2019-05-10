create table Results(
id int PRIMARY KEY NOT NULL IDENTITY,
Result_Qual1 int,
Result_Qual2 int,
Result_Qual3 int,
Result_Qual4 int
);

create table Complexity(
id int PRIMARY KEY NOT NULL IDENTITY,
Complexity_Qual1 int,
Complexity_Qual2 int,
Complexity_Qual3 int,
Complexity_Qual4 int
);

create table Qualifications(
id int PRIMARY KEY NOT NULL IDENTITY,
Name varchar(30) not null,
Coefficient decimal(2,1) not null
);

create table Type(
id int PRIMARY KEY NOT NULL IDENTITY,
Name varchar(20) not null
);

create table Status(
id int PRIMARY KEY NOT NULL IDENTITY,
Name varchar(20) not null
);

create table Tasks(
id int PRIMARY KEY NOT NULL IDENTITY,
Name varchar(200) not null,
id_ParentTask int,
Description text,
id_Complexity int not null,
Date_Delivery date not null,
id_TaskManager int not null,
FOREIGN KEY (id_ParentTask) REFERENCES Tasks (id),
FOREIGN KEY (id_Complexity) REFERENCES Complexity (id) ON DELETE CASCADE
);

create table Employees(
id int PRIMARY KEY NOT NULL IDENTITY,
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
id int PRIMARY KEY NOT NULL IDENTITY,
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
id int PRIMARY KEY NOT NULL IDENTITY,
Date datetime not null,
id_LastStatus int,
id_CurrentStatus int,
id_Employee int not null,
id_Task int not null,
FOREIGN KEY (id_LastStatus) REFERENCES Status (id),
FOREIGN KEY (id_CurrentStatus) REFERENCES Status (id),
FOREIGN KEY (id_Employee) REFERENCES Employees (id),
FOREIGN KEY (id_Task) REFERENCES Tasks (id)
);

insert into Qualifications(Name, Coefficient) values
	('3-category engineer', 1.0),('2-category engineer', 1.2),
	('1-category engineer', 1.4),('Chief Engineer', 1.5);
	
insert into Status(Name) values
	('Created'),('Assigned'),
	('On execution'),('Suspended'),
	('Completed');

insert into Complexity(Complexity_Qual1,Complexity_Qual2,Complexity_Qual3,Complexity_Qual4) values
	(0, 10, 10, 0),(30, 20, 0, 0),
	(22, 25, 10, 0),(0, 0, 0, 35),
	(60,0,0,0),(20,0,15,0);

insert into Type(Name) values ('Admin'),('Director'),('User');

insert into Employees(FIO, DateOfBirth, id_Qualification, Login, Password, id_Type) values 
	('Dynin Nick Vadimovich', '1996-01-16', 4, 'Admin','Admin', 1),
	('Alekseev Dima Andreevich', '1996-09-24', 3, 'abdc50','123456',2), 
	('Svechnikov Egor Aleksandrovich', '1995-06-15', 1, 'user','user',3); 

insert into Tasks(Name, id_Complexity, Date_Delivery, id_TaskManager) values
	('Automated Information Management Systems', 1, '2019-06-15', 1),
	('Purpose of the adaptive matrix multiplier', 2, '2019-05-31', 1),
	('Adequacy and objectivity of modeling an information management system', 3, '2019-05-16', 1),
	('The monitoring algorithm of the state of the data transmission network', 4, '2019-05-19', 1),
	('The essence and features of the algorithm of the data input and processing unit', 5, '2019-05-26', 1), 
	('Estimation of traffic intensity', 6, '2019-06-05', 1);