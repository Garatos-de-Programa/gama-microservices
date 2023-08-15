export GAMA_CORE_NAME=gama-core-api
export GAMA_CORE_PATH=src/services/GamaCore
export GAMA_CORE_REPO_NAME=gama-microservices-repo
export NATIONAL_GMESSAGER_NAME=national-geo-messager-api
export NATIONAL_GMESSAGER_PATH=../services/NationalGeographicMessager
export NATIONAL_GMESSAGER_REPO_NAME=gama-microservices-repo
export NATIONAL_GWORKER_NAME=national-geo-worker-api
export NATIONAL_GWORKER_PATH=services/NationalGeographicWorker
export NATIONAL_GWORKER_REPO_NAME=gama-microservices-repo
export ACCOUNT_NUMBER= 737687692117#$$(aws sts get-caller-identity --output  text --query 'Account') 
export AWS_DEFAULT_REGION=us-east-2
export ECR_URL=${ACCOUNT_NUMBER}.dkr.ecr.${AWS_DEFAULT_REGION}.amazonaws.com
export ALB_URL=$$(terraform output -json | jq -r '.url.value')

repo:
	aws ecr create-repository --repository-name ${GAMA_CORE_REPO_NAME}

log-repo:
	echo ${ECR_URL}

login:
	aws ecr get-login-password --region ${AWS_DEFAULT_REGION} | docker login --username AWS --password-stdin ${ECR_URL}

image:
	docker build --rm --pull -f ${GAMA_CORE_PATH}/Dockerfile -t ${GAMA_CORE_REPO_NAME} .
	docker tag ${GAMA_CORE_REPO_NAME}:latest ${ECR_URL}/${GAMA_CORE_REPO_NAME}:latest
	docker push ${ECR_URL}/${GAMA_CORE_REPO_NAME}:latest

init:
	terraform init infra

plan:
	terraform plan infra

apply:
	terraform apply -auto-approve infra

kill:
	terraform destroy -auto-approve infra
	aws ecr list-images --repository-name ${GAMA_CORE_REPO_NAME} --query 'imageIds[*].imageDigest' --output text | while read imageId; do aws ecr batch-delete-image --repository-name ${GAMA_CORE_REPO_NAME} --image-ids imageDigest=$$imageId; done
	aws ecr delete-repository --repository-name ${GAMA_CORE_REPO_NAME}