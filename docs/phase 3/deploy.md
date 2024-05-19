# Deployment process overview
We use Azure Pipeline to deploy the apllication.

The pipeline:
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/41050d30-acbe-4dd2-a59e-52f5b722b0a4)

Kubernetes pod is running:
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/77027e00-cd3d-411f-b85b-5e450aa43ac2)

You can find the whole pipeline definition here: [azure-pipeline.yml](https://github.com/oleksandrmanetskyi/SportsHub/blob/main/SportsHub/azure-pipelines.yml)

This pipeline trigers on each commit to the `main` and `dev` branches:
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/d973c80c-477e-401b-b22c-2e83a801e641)

# CI
The CI process has the following steps:
1. Install NuGet and restore packages
2. Build the project
3. Run tests and publish code coverage
4. Install GitVersion tool to assign a specific version to the build
5. Build docker image using Dockerfile
6. Push docker image to registry created on Azure

The Docker registry created on Azure:
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/90b68cbb-855a-481e-b675-b4febad9917e)

# Deploy
The deploy process has the following steps:
1. Install Helm
2. Package the app and upgrade it on Kubernetes. Helm config files are located here: (charts/sportshub)[https://github.com/oleksandrmanetskyi/SportsHub/tree/main/SportsHub/SportsHub/charts/sportshub]

Azure Kubernetes Service where app is running:
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/296de2cd-93d3-428a-86b7-4b03cfe054b3)

The database is also deployed to Azure:
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/5db4d5bd-9764-4cf6-b21d-6c53fac93b79)

# Tests and Code coverage
The pipeline includes running tests. We also collect Code Coverage during the test run:
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/458d079d-053d-4053-859b-58c437090271)
![image](https://github.com/oleksandrmanetskyi/SportsHub/assets/56626861/bb117053-e848-4c3f-acc8-4e9b3fe93a5a)
