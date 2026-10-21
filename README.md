# 📋 Minha Agenda de Contatos

Uma aplicação web desenvolvida com **ASP.NET Core MVC** para gerenciar contatos de forma simples, intuitiva e responsiva. Organize seus contatos pessoais e profissionais em categorias, mantendo todas as informações em um só lugar.

## 📌 Sobre

**Agenda de Contatos** é uma aplicação web completa que permite:
- Cadastrar e gerenciar contatos com informações de nome, telefone, e-mail e tipos
- Organizar contatos em categorias predefinidas (Família, Trabalho, Amigos e Outros)
- Buscar contatos instantaneamente por nome, telefone ou e-mail
- Armazenar dados persistentemente em banco de dados local (SQLite)
- Acessar a aplicação de forma responsiva em diferentes dispositivos

A aplicação foi desenvolvida seguindo padrões modernos de desenvolvimento web, com arquitetura MVC, validação de dados e uma interface amigável ao usuário.

## ✨ Características

- ✅ **CRUD Completo**: Criar, ler, atualizar e deletar contatos
- ✅ **Categorização**: Organize contatos em categorias predefinidas
- ✅ **Busca Instantânea**: Filtro em tempo real de contatos por nome, telefone ou e-mail
- ✅ **Validação de Dados**: Validação client-side e server-side com mensagens de erro claras 
- ✅ **Design Responsivo**: Interface adaptável para mobile, tablet e desktop
- ✅ **Persistência de Dados**: Armazenamento em banco de dados SQLite local
- ✅ **Navegação Intuitiva**: Menu com categorias e transição suave entre seções
- ✅ **Feedback Visual**: Mensagens de sucesso e erro para ações do usuário
- ✅ **Edição em Modal**: Formulário de contato em modal para melhor UX

## 🛠️ Tecnologias Utilizadas

### Frontend
- **Razor Views** - Template engine do ASP.NET
- **Bootstrap 5** - Framework CSS responsivo
- **Javascript** - Linguagem de Programação                                                       
- **CSS Customizado** - Estilos personalizados com design system próprio
- **HTML5** - Markup semântico

### Backend
- **ASP.NET Core 10.0** - Framework web moderno e de alto desempenho
- **C#** - Linguagem de programação
- **Entity Framework Core 10.0** - ORM para acesso a dados
- **SQLite** - Banco de dados leve e portável

### Ferramentas de Desenvolvimento
- **.NET 10.0 SDK** - Kit de desenvolvimento
- **Entity Framework Core Tools** - Ferramentas para migrations

## 🎨 Layout/Designer + Responsividade

A aplicação apresenta um design moderno e minimalista com as seguintes características visuais:

- **Hero Panel**: Seção de boas-vindas na página inicial com destaque visual
- **Card-based Layout**: Categorias apresentadas em cards com cores distintivas
- **Responsividade Mobile-First**: Layout adaptável para todos os tamanhos de tela
- **Tabela Responsiva**: Listagem de contatos com comportamento adaptativo
- **Modal Intuitivo**: Formulário em modal para criação e edição de contatos
- **Sistema de Cores**: Cada categoria possui uma cor própria para melhor identificação
- **Feedback Visual**: Ícones, animações e mensagens para feedback do usuário

## 📚 Conceitos Aplicados

- **Arquitetura MVC** - Separação clara entre Models, Views e Controllers
- **Entity Framework Core** - Mapeamento objeto-relacional com LINQ
- **Async/Await** - Operações assíncronas para melhor performance
- **Validação de Dados** - Usando Data Annotations no modelo
- **Dependency Injection** - Padrão de injeção de dependências nativo do ASP.NET
- **Entity Framework Migrations** - Versionamento e evolução do banco de dados
- **RESTful Routing** - URLs descritivas e amigáveis
- **SPA-like Behavior** - Atualizações de conteúdo sem recarregar a página (partial updates)

## ⚙️ Versão do .NET

- **.NET 10.0**
- **C# 13+** 
- **SQLite** como banco de dados padrão

**Requisitos do Sistema:**
- .NET 10.0 SDK ou Runtime instalado
- Mínimo 100 MB de espaço em disco
- Qualquer navegador web moderno

## 🚀 Como Baixar e Rodar o Projeto

### Pré-requisitos
Antes de começar, certifique-se de ter instalado:
- **.NET 10.0 SDK** - [Baixar aqui](https://dotnet.microsoft.com/download)
- **Git** (opcional, para clonar o repositório)

### Passo 1: Clonar o Repositório
```bash
git clone https://github.com/DiegoRamonSantos/AgendaContatos.git
cd AgendaContatos
```
Ou se preferir, baixe o repositório como ZIP e extraia na pasta desejada.

### Passo 2: Restaurar as Dependências

Ao abrir `AgendaContatos.sln` no Visual Studio 2026, os pacotes NuGet serão restaurados automaticamente. Se preferir, também é possível executar `dotnet restore` no terminal, dentro da pasta do projeto.

### Passo 3: Executar a Aplicação no Visual Studio 2026

1. Abra o arquivo `AgendaContatos.sln` no Visual Studio 2026.
2. Aguarde a restauração dos pacotes NuGet. Se solicitado, confirme a restauração.
3. Na barra superior, selecione o perfil `https` e pressione **F5** para iniciar com depuração ou **Ctrl+F5** para iniciar sem depuração.

O Visual Studio abrirá a aplicação no navegador. Se isso não acontecer, acesse `https://localhost:7192` ou `http://localhost:5281`.

Caso o navegador não abra automaticamente, acesse manualmente:
http://localhost:5281

### Passo 4: Usar a Aplicação
1. Na página inicial, selecione uma categoria (**Família**, **Trabalho**, **Amigos** ou **Outros**).
2. Clique em "**Novo Contato**" para adicionar um novo contato.
3. Preencha os dados: Nome, Telefone, E-mail e selecione uma opção (opcional).
4. Clique em "**Salvar**" para guardar o contato.
5. Use a barra de busca para filtrar contatos instantaneamente.
6. Clique em um contato na tabela para editar ou deletar.

### Parar a Aplicação
Pressione `Ctrl + C` no terminal onde a aplicação está rodando.

## 📝 Observações Importantes
- **Banco de Dados Local**: Os dados são armazenados em um banco SQLite local. Não há sincronização em nuvem.
- **Variáveis de Ambiente**: A aplicação detecta automaticamente o ambiente (**Development/Production**) e ajusta o comportamento.
- **Conexão**: Configurável no arquivo `appsettings.json` via string de conexão `AgendaContatosDb`.

## 👨‍💻 Autor do Projeto
- **Nome**: Diego Ramon 