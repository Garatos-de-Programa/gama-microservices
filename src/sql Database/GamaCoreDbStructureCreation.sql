CREATE TABLE users (
	id SERIAL PRIMARY KEY,
	first_name VARCHAR(30) NOT NULL,
	last_name VARCHAR(30) NOT NULL,
	username VARCHAR(50) NOT NULL UNIQUE,
	email VARCHAR(128) NOT NULL UNIQUE,
	password TEXT NOT NULL,
	document_number CHAR(11) NOT NULL,
	active bool NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP,
	deleted_at TIMESTAMP
);

COMMIT;

CREATE TABLE user_address (
	id SERIAL PRIMARY KEY,
	user_id INT NOT NULL,
	zip_code CHAR(8) NOT NULL,
	street VARCHAR(50) NOT NULL,
	city VARCHAR(50) NOT NULL,
	state CHAR(2) NOT NULL,
	district VARCHAR(50) NOT NULL,
	modified_by VARCHAR(50),
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP,
	CONSTRAINT fk_user
      FOREIGN KEY(user_id) 
	  	REFERENCES users(id)
);

COMMIT;

CREATE TABLE roles (
	id SERIAL PRIMARY KEY,
	name VARCHAR(15) NOT NULL
);

COMMIT;


INSERT INTO public.roles (name) VALUES ('Cop'), ('Admin'), ('Citizen');

COMMIT;

CREATE TABLE user_roles (
	user_id INT NOT NULL,
	role_id INT NOT NULL,
	PRIMARY KEY(user_id, role_id),
	CONSTRAINT fk_user
      FOREIGN KEY(user_id) 
	  	REFERENCES users(id),
	CONSTRAINT fk_role
      FOREIGN KEY(role_id) 
	  	REFERENCES roles(id)
);

COMMIT;

CREATE TABLE traffic_violations (
	id SERIAL PRIMARY KEY,
	code VARCHAR(20) NOT NULL,
	name VARCHAR(50) NOT NULL,
	modified_by VARCHAR(50) NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP
);

COMMIT;

CREATE TABLE traffic_fines (
	id SERIAL PRIMARY KEY,
	license_plate VARCHAR(50) NOT NULL,
	latitude numeric(10, 8) NOT NULL,
  	longitude numeric(11, 8) NOT NULL,
	active bool NOT NULL,
	computed bool NOT NULL,
	modified_by VARCHAR(50) NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP,	
	deleted_at TIMESTAMP
);

COMMIT;

CREATE TABLE traffic_fine_traffic_violations (
	traffic_fine_id INT NOT NULL,
	traffic_violation_id INT NOT NULL,
	PRIMARY KEY(traffic_fine_id, traffic_violation_id),
	CONSTRAINT fk_traffic_fine
      FOREIGN KEY(traffic_fine_id) 
	  	REFERENCES traffic_fines(id),
	CONSTRAINT fk_traffic_violation
      FOREIGN KEY(traffic_violation_id) 
	  	REFERENCES traffic_violations(id)
);

COMMIT;

CREATE TABLE occurrence_status (
	id SMALLSERIAL PRIMARY KEY,
	name VARCHAR(20) NOT NULL
);

COMMIT;

CREATE TABLE occurrence_types (
	id SMALLSERIAL PRIMARY KEY,
	name VARCHAR(20) NOT NULL
);

COMMIT;

CREATE TABLE occurrence_urgency_levels (
	id SMALLSERIAL PRIMARY KEY,
	name VARCHAR(20) NOT NULL
);

COMMIT;


CREATE TABLE occurrences (
	id SERIAL PRIMARY KEY,
	latitude numeric(10, 8) NOT NULL,
  	longitude numeric(11, 8) NOT NULL,
	location VARCHAR(100) NOT NULL,
	name VARCHAR(50) NOT NULL,
	occurrence_status_id SMALLINT NOT NULL,
	occurrence_type_id SMALLINT NOT NULL,
	occurrence_urgency_level_id SMALLINT NOT NULL,
	description VARCHAR(200) NOT NULL,
	user_id INT NOT NULL,
	active bool NOT NULL,
	modified_by VARCHAR(50) NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP,	
	deleted_at TIMESTAMP,
	CONSTRAINT fk_occurrence_status
      FOREIGN KEY(occurrence_status_id) 
	  	REFERENCES occurrence_status(id),
	CONSTRAINT fk_occurrence_type
      FOREIGN KEY(occurrence_type_id) 
	  	REFERENCES occurrence_types(id),
	CONSTRAINT fk_occurrence_urgency_level
      FOREIGN KEY(occurrence_urgency_level_id) 
	  	REFERENCES occurrence_urgency_levels(id),
	CONSTRAINT fk_user_id
      FOREIGN KEY(user_id) 
	  	REFERENCES users(id)
);

COMMIT;


CREATE OR REPLACE FUNCTION occurrences_notification_function() RETURNS trigger AS $$
DECLARE
    row RECORD;
    output TEXT;
	occurrence_urgency_level TEXT;
	occurrence_type TEXT;
BEGIN
	IF (TG_OP = 'DELETE') THEN
      row = OLD;
    ELSE
      row = NEW;
    END IF;
	
	SELECT name INTO occurrence_urgency_level FROM public.occurrence_urgency_levels WHERE id = row.occurrence_urgency_level_id;
	SELECT name INTO occurrence_type FROM public.occurrence_types WHERE id = row.occurrence_type_id;

	output = 'id:' || row.id || ';latitude:' || row.latitude 
				|| ';longitude:' || row.longitude || ';name:' 
				|| row.name || ';active:' || row.active 
				|| ';occurrenceUrgencyLevel:' || occurrence_urgency_level 
				|| ';occurrenceType:' || occurrence_type;
	
  	PERFORM pg_notify('occurrence_notification',output);
  
  RETURN NULL;
END;
$$ LANGUAGE plpgsql;



COMMIT;


CREATE TRIGGER occurrences_event_trigger
AFTER INSERT OR UPDATE OR DELETE
ON public.occurrences
FOR EACH ROW
EXECUTE FUNCTION occurrences_notification_function();






