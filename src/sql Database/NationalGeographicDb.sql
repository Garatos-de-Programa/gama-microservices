CREATE TABLE occurrences (
    occurrence_id INT PRIMARY KEY,
    geolocation GEOGRAPHY(POINT),
    location VARCHAR(100) NOT NULL,
    occurrence_name VARCHAR(50) NOT NULL,
    status_name VARCHAR(50) NOT NULL,
    occurrence_type_name VARCHAR(50) NOT NULL,
    occurrence_urgency_level_name VARCHAR(50) NOT NULL,
    user_id INT NOT NULL,
    user_name VARCHAR(50) NOT NULL,
    active bool NOT NULL
);
