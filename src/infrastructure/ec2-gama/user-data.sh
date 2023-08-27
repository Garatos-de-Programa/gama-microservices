
sudo yum  update -y && yum  upgrade -y

sudo yum  install -y docker
sudo yum  install -y amazon-ecr-credential-helper
mkdir -p ~/.docker && chmod 0700 ~/.docker
echo '{"credsStore": "ecr-login"}' > ~/.docker/config.json

sudo service docker start

sudo usermod -a -G docker ec2-user

sudo service docker restart

sudo $(aws ecr get-login --no-include-email --region us-east-2)

sudo docker run --restart=always -d -p 80:80 737687692117.dkr.ecr.us-east-2.amazonaws.com/gama-microservices-repo:latest