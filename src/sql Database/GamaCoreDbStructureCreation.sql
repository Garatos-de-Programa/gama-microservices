CREATE IF NOT EXISTS TABLE users (
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


CREATE IF NOT EXISTS TABLE roles (
	id SERIAL PRIMARY KEY,
	name VARCHAR(15) NOT NULL
);

COMMIT;


INSERT INTO public.roles (name) 
VALUES 
	('Cop'), 
	('Admin'),
	('Citizen')
ON CONFLICT (name) DO NOTHING;;

COMMIT;

CREATE IF NOT EXISTS TABLE user_roles (
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

CREATE IF NOT EXISTS TABLE traffic_violations (
	id SERIAL PRIMARY KEY,
	code VARCHAR(20) NOT NULL,
	name VARCHAR(50) NOT NULL,
	active bool NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP,
	deleted_at TIMESTAMP
);

COMMIT;

CREATE IF NOT EXISTS TABLE traffic_fines (
	id SERIAL PRIMARY KEY,
	license_plate CHAR(7) NOT NULL,
	latitude numeric(10, 8) NOT NULL,
  	longitude numeric(11, 8) NOT NULL,
	active bool NOT NULL,
	computed bool NOT NULL,
	user_id INT NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP,	
	deleted_at TIMESTAMP,
	CONSTRAINT fk_user
      FOREIGN KEY(user_id) 
	  	REFERENCES users(id)
);

COMMIT;

CREATE IF NOT EXISTS TABLE traffic_fine_traffic_violations (
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

CREATE IF NOT EXISTS TABLE occurrence_status (
	id SMALLSERIAL PRIMARY KEY,
	name VARCHAR(20) NOT NULL
);

COMMIT;

CREATE IF NOT EXISTS TABLE occurrence_types (
	id SMALLSERIAL PRIMARY KEY,
	name VARCHAR(20) NOT NULL
);

COMMIT;

CREATE IF NOT EXISTS TABLE occurrence_urgency_levels (
	id SMALLSERIAL PRIMARY KEY,
	name VARCHAR(20) NOT NULL
);

COMMIT;


CREATE IF NOT EXISTS TABLE occurrences (
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








