use HumanArc;

drop table if exists UserQuizQuestionAnswers;
drop table if exists Answers;
drop table if exists Questions;
drop table if exists Quizes;
drop table if exists Groups;
drop table if exists Users;

create table Users(
	id int identity primary key,
	firstName varchar(50) not null,
	lastName varchar(50) not null,
	email varchar(100),
	userGroups varchar(max),
	SAMAccountName varchar(100) not null
);

create table Groups(
	id int identity primary key,
	name varchar(100) not null
);

create table Quizes(
	id int identity primary key,
	groupId int not null,
	title varchar(100) not null,
	description varchar(max),
	media varchar(max),
	foreign key (groupId) references Groups(id)
);

create table Questions(
	id int identity primary key,
	quizId int not null,
	questionText varchar(max) not null,
	questionType varchar(50) not null,
	foreign key (quizId) references Quizes(id)
);

create table Answers(
	id int identity primary key,
	questionId int not null,
	answerText varchar(500) not null,
	isCorrect bit not null,
	foreign key (questionId) references Questions(id)
);

create table UserQuizQuestionAnswers(
	id int identity primary key,
	quizId int not null,
	questionId int not null,
	answerId int not null,
	isChecked bit,
	isApproved bit,
	foreign key (quizId) references Quizes(id),
	foreign key (questionId) references Questions(id),
	foreign key (answerId) references Answers(id)
);