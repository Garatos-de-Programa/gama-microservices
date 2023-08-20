locals {
  region = "us-east-2"
  context = {
    prd = {
        allocated_storage = 5,
        instance_class = "db.t3.micro",
        db_name = "gama"
        storage_type = "gp2"
    }
  }
  
  vpc_tag = "gama-microservice-vpc"
  subnet_tag = "gama-microservice-subnet"
  subnet_tag_b = "gama-microservice-subnet-b"

  identifier = "gama-core-db"
}