create table Priority( 
	id int PRIMARY KEY NOT NULL IDENTITY, 
	Name varchar(50) not null 
);

create table Qualifications( 
	id int PRIMARY KEY NOT NULL IDENTITY, 
	Name varchar(50) not null, 
	Coefficient int not null 
); 

create table Positions( 
	id int PRIMARY KEY NOT NULL IDENTITY, 
	Name varchar(50) not null 
); 

create table Type( 
	id int PRIMARY KEY NOT NULL IDENTITY, 
	Name varchar(50) not null 
);

create table Results(
	id int PRIMARY KEY NOT NULL IDENTITY,
	Name varchar(30) not null
);

create table Employees( 
	id int PRIMARY KEY NOT NULL IDENTITY, 
	FIO varchar(150) not null, 
	DateOfBirth date not null, 
	id_Qualification int not null, 
	id_Position int not null, 
	Login varchar(20) not null unique, 
	Password varchar(40) not null unique, 
	id_Type int not null, 
	FOREIGN KEY (id_Qualification) REFERENCES Qualifications (id) ON DELETE CASCADE, 
	FOREIGN KEY (id_Position) REFERENCES Positions (id) ON DELETE CASCADE, 
	FOREIGN KEY (id_Type) REFERENCES Type (id) ON DELETE CASCADE 
);


create table Tasks( 
	id int PRIMARY KEY NOT NULL IDENTITY, 
	Name varchar(255) not null, 
	id_ParentTask int, 
	Description text, 
	Complexity int not null, 
	Date_Delivery date not null, 
	id_TaskManager int not null, 
	id_Priority int not null, 
	FOREIGN KEY (id_ParentTask) REFERENCES Tasks (id), 
	FOREIGN KEY (id_Priority) REFERENCES Priority (id) ON DELETE CASCADE 
);

create table AssignedTasks( 
	id int PRIMARY KEY NOT NULL IDENTITY, 
	id_Task int not null, 
	id_Employee int not null, 
	Date_Start date not null, 
	Date_End date, 
	id_Result int not null, 
	Comment text, 
	FOREIGN KEY (id_Task) REFERENCES Tasks (id) ON DELETE CASCADE, 
	FOREIGN KEY (id_Employee) REFERENCES Employees (id) ON DELETE CASCADE, 
	FOREIGN KEY (id_Result) REFERENCES Results (id) ON DELETE CASCADE 
);

insert into Qualifications(Name, Coefficient) values
('Неквалифицированный', 1), ('Малоквалифицированный', 2),
('Квалифицированный', 4), ('Высококвалифицированный', 6); 

insert into Positions(Name) values
('Директор'),('Водитель'),
('Младший инженер'),('Старший инженер'),
('Начальник участка'); 

insert into Priority(Name) values ('Низкий'),('Средний'),('Высокий'),('Срочный'); 

insert into Type(Name) values ('Admin'),('Director'),('User');

insert into Employees(FIO, DateOfBirth, id_Qualification, id_Position, Login, Password, id_Type) values 
('Дынин Николай Вадимович', '1996-01-16', 4, 1, 'Admin','Admin', 1),
('Алексеев Дмитрий Андреевич', '1996-09-24', 3, 4, 'abdc50','123456',2), 
('Свечников Егор Александрович', '1995-06-15', 1, 2, 'user','user',3); 

insert into Tasks(Name, Complexity, Date_Delivery, id_TaskManager, id_Priority) values
('Автоматизированные информационные системы управления', 50, '2019-06-01', 1, 3),
('Предназначение адаптивного матричного мультипликатора', 60, '2019-05-05', 1, 1),
('Адекватность и объективность моделирования информационной системы управления', 30, '2019-04-19', 1, 2),
('Мониторинговый алгоритм состояния сети передачи данных', 20, '2019-04-20', 1, 2),
('Сущность и особенности алгоритма работы блока ввода и обработки данных', 5, '2019-05-07', 1, 3), 
('Оценка интенсивности трафика', 40, '2019-05-10', 1, 2),
('Оценка качества связи на основании принятия информационных сигналов', 15, '2019-07-25', 1, 4), 
('Особенности математического и программного обеспечения систем управления', 10,'2019-06-07', 1, 1),
('Оценка надежности системы с общей резервацией элементов', 10, '2019-05-15', 1, 3),
('Характерные особенности моделирования систем передачи информации особой важности', 50, '2019-08-01', 1, 1);

insert into Results(Name) values
('Начинание'),('Планирование'),
('Принятие решения'),('Выполнение'),
('Оценка результатов'); 

insert into AssignedTasks(id_Task, id_Employee, Date_Start, id_Result) values 
(1,2,'2019-04-01',1), (2,3,'2019-04-03',2),
(3,3,'2019-04-10',4), (4,1,'2019-03-15', 3),
(5,2,'2019-03-20', 1),(6,3,'2019-04-09',5),
(7,3, '2019-04-10', 4),(8,2, '2019-03-20', 2),
(9,2, '2019-04-01', 4),(10,3, '2019-04-30', 4);