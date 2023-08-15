set -e

exec > >(tee /var/log/user-data.log|logger -t user-data -s 2>/dev/console) 2>&1

yum update -y && yum upgrade -y

yum install -y docker amazon-ecr-credential-helper

mkdir -p ~/.docker && chmod 0700 ~/.docker
echo '{"credsStore": "ecr-login"}' > ~/.docker/config.json

service docker start && chkconfig docker on

usermod -a -G docker ec2-user

docker run --restart=always -d -p 80:80 737687692117.dkr.ecr.us-east-2.amazonaws.com/gama-microservices-repo:latest