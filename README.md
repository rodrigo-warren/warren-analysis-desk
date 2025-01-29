# Tecnologias

## API

<img src="https://w7.pngwing.com/pngs/240/85/png-transparent-c.png" height="40" />

**C#** é uma linguagem de programação, multiparadigma, de tipagem forte, desenvolvida pela Microsoft como parte da plataforma .NET

<img src="https://static-00.iconduck.com/assets.00/swagger-icon-1024x1024-09037v1r.png" width="30" height="30" />

**Swagger** - Uma ferramenta para projetar, construir, documentar e usar APIs RESTful.

## Infraestrutura

<img src="https://www.proficom.de/blog//app/uploads/2018/09/Docker.png" style="margin-left: -12px" height="30" />

**Docker** - Uma plataforma aberta para desenvolver, enviar e executar aplicativos.

<img src="https://github.githubassets.com/images/modules/logos_page/GitHub-Mark.png" width="30" height="30" />

**GitHub Actions (CI/CD)** - Uma solução de CI/CD integrada ao GitHub.

# 🔒 Microserviço Base (MB)
## Requisitos funcionais
    - Conectar a API da EZZE [ Auto Frota ].
    - Popular o banco de dados próprio caso não exista a informação.
    - A informação será consumida direito da nossa API e repassada ao destino solicitado.

## Requisitos não funcionais
    - O sistema deve garantir a segurança dos dados trafegados.
    - O sistema deve rotar com NetCore 8.
    - O sistema terá sua base de dados própria em SQL

# Instalação do Microserviço Local

### Certifique-se de ter instalado

- Docker 
- .NET
- Git

### Primeiro passo

Abra seu terminal e clone este projeto

```shell
git clone https://github.com/rodrigo-warren/warren-analysis-desk.git
```

Inicie a imagem do SQL Server no docker 

```shell
docker pull mysql
```

Feito isso inicie o container 

```shell
docker run --name <NOME_CONTAINER> -e MYSQL_ROOT_PASSWORD=<SENHA_BANCO> -p 3306:3306 -d mysql
```

Depois de rodar os comandos para construção do banco, inicie a construção, para isso acesse a raiz do projeto e execute no terminal

```
dotnet tool install --global dotnet-ef
```

```shell
dotnet ef database update
```

Feito isso você pode iniciar o microserviço

```shell
dotnet run
```
- Lembre-se de editar o appsettings.json com as configurações que você definiu ao criar o container do banco sql server

- Rota padrão: **http://localhost:5151/swagger/index.html**