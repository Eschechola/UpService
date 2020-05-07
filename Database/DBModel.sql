create database upservice;


desc up_client;

create table up_client(
	id int primary key auto_increment,
    client_name varchar(80),
    client_password varchar(10),
    client_email varchar(120) unique,
    client_cpf varchar(15) unique,
    client_telephone varchar(15),
    client_type enum('PS', 'SS'),
    client_country varchar(60),
    client_state varchar(90),
    client_city varchar(90),
    client_zip_code varchar(15),
    client_street varchar(150),
    client_home_number int
)charset=utf8;


create table up_job(
	id int primary key auto_increment,
    fk_id_client_job_requester int not null,
    fk_id_client_job_provider int,
    job_hash varchar(50),
    job_title varchar(90),
    job_description varchar(3000),
    job_publication_date datetime,
    job_conclusion_date datetime,
    job_max_value double,
    job_state enum('PB', 'AC', 'FS')
)charset=utf8;

ALTER TABLE up_job ADD CONSTRAINT fk_id_client_service_requester
FOREIGN KEY ( id ) REFERENCES up_client ( id );

ALTER TABLE up_job ADD CONSTRAINT fk_id_client_service_provider
FOREIGN KEY ( id ) REFERENCES up_client ( id );


