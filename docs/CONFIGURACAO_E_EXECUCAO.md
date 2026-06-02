# Guia Detalhado de Configuração e Execução

Este documento fornece instruções passo a passo, em detalhes, sobre como preparar o ambiente local, configurar o banco de dados MySQL e rodar a aplicação "Nova Economia Espacial" utilizando a orquestração do .NET Aspire.

---

## 1. Pré-requisitos do Sistema

Antes de começar, certifique-se de ter os seguintes softwares instalados na sua máquina:
1. **.NET SDK**: Requer a versão 8.0, 9.0 ou superior (o projeto atualmente roda na arquitetura do .NET 10). Você pode verificar abrindo o terminal e digitando `dotnet --version`.
2. **MySQL Server**: Um servidor de banco de dados MySQL rodando localmente (porta padrão `3306`).
3. **IDE (Opcional, mas recomendado)**: Visual Studio 2022, Visual Studio Code (com as extensões do C#) ou JetBrains Rider.

---

## 2. Configurando o Banco de Dados (MySQL)

O projeto utiliza o **Entity Framework Core** com abordagem *Code-First*. Isso significa que você **não precisa criar as tabelas manualmente** no MySQL; o próprio código fará isso. No entanto, é estritamente necessário configurar as suas credenciais locais para que a aplicação consiga se comunicar com o seu servidor MySQL.

### 2.1. Localizando o Arquivo de Configuração
1. Abra a pasta principal do projeto (`GS2-Csharp`).
2. Navegue até a pasta da API: `GlobalSolutionSpace/ProjetoGS.ApiService`.
3. Abra o arquivo **`appsettings.json`** (ou `appsettings.Development.json`).

### 2.2. Alterando a Connection String
Dentro do arquivo, você encontrará um bloco chamado `"ConnectionStrings"`. Ele se parece com isso:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=gs_db;Uid=root;Pwd=root;"
}
```

- `Server=localhost`: Mantém-se inalterado se o MySQL estiver rodando no mesmo computador.
- `Database=gs_db`: O nome do banco de dados que será criado automaticamente. Pode manter como está.
- `Uid=root`: Troque `root` pelo nome do usuário do seu MySQL local.
- `Pwd=root`: Troque `root` pela senha real que você usa no seu MySQL local.

*Salve o arquivo após realizar a alteração.*

---

## 3. Criando o Banco de Dados (Migrations)

Agora que as credenciais estão corretas, precisamos enviar o comando para que a aplicação crie as tabelas dentro do seu MySQL.

1. Abra o **Terminal** (ou Prompt de Comando/PowerShell).
2. Navegue até a pasta do projeto de API usando o comando `cd`:
   ```bash
   cd caminho/para/seu/GS2-Csharp/GlobalSolutionSpace/ProjetoGS.ApiService
   ```
3. Execute o comando de atualização do banco de dados:
   ```bash
   dotnet ef database update
   ```
   *Nota: Se o comando `dotnet ef` não for reconhecido, instale a ferramenta globalmente rodando: `dotnet tool install --global dotnet-ef`.*

Se tudo der certo, você verá a mensagem `Done.` no terminal. Isso indica que todas as tabelas (Usuários, Tecnologias, Origens, etc.) foram criadas no MySQL com sucesso.

---

## 4. Rodando o Projeto (Orquestração .NET Aspire)

Um dos grandes diferenciais arquiteturais deste projeto é o uso do **.NET Aspire**. Isso significa que você **não deve tentar rodar a API e o Front-End separadamente**. Você deve rodar o orquestrador (o projeto `AppHost`), e ele se encarregará de subir a API, o Front-End, injetar as variáveis de ambiente corretas e configurar a comunicação via Service Discovery.

### Passo a Passo da Execução:

1. No seu Terminal, navegue até a pasta do `AppHost`:
   ```bash
   cd caminho/para/seu/GS2-Csharp/GlobalSolutionSpace/GlobalSolutionSpace.AppHost
   ```

2. Execute o comando de compilação e execução:
   ```bash
   dotnet run
   ```

3. O processo de "Build" vai iniciar. Após alguns segundos, o terminal exibirá logs indicando que a aplicação está rodando. Procure pela linha que diz:
   ```text
   Login to the dashboard at https://localhost:XXXXX/login?t=seu-token-aqui
   ```

4. **Abra esse link (Dashboard do Aspire) no seu navegador.**
   - O painel de controle do Aspire vai carregar.
   - Na aba **"Resources"** (Recursos), você verá dois projetos rodando: `apiservice` e `webfrontend`.
   - Na linha referente ao `webfrontend`, procure a coluna **"Endpoints"** e clique no link principal (ex: `https://localhost:7194`).

5. **Pronto!** A Landing Page do projeto será carregada no seu navegador. O projeto está 100% no ar.

---

## 5. Como Acessar o Sistema e Testar

Durante a inicialização da aplicação (Graças ao `DatabaseSeeder`), o sistema já foi abastecido automaticamente com categorias espaciais e uma conta Administrativa.

**Para acessar o Dashboard de Estatísticas e Gerenciamento:**
1. Clique no botão **"Acessar Plataforma"** (ou "Entrar" na barra de navegação).
2. Insira as credenciais padrão de Administrador criadas pelo sistema:
   - **Email:** `admin@novaeconomia.space`
   - **Senha:** `Admin@123`

Parabéns! Você já está logado, com acesso total ao sistema de CRUD e gráficos e pode começar a adicionar novas inovações no painel.
