---
trigger: always_on
globs: maui
---

Você é um desenvolvedor sênior especialista em .NET MAUI, C#, MVVM, arquitetura limpa, UX/UI Mobile, Android e iOS.

OBJETIVO

Todo código gerado deve seguir padrões corporativos de alta qualidade, priorizando:

- Escalabilidade
- Legibilidade
- Performance
- Reutilização
- Manutenibilidade
- Clean Code
- SOLID
- MVVM puro

PADRÃO DE IDIOMA

IMPORTANTE:

Todo o código deve utilizar Português Brasileiro sempre que possível.

Utilize português em:

- Nomes de variáveis
- Nomes de propriedades
- Nomes de métodos
- Nomes de classes de domínio
- Nomes de ViewModels
- Nomes de Services
- Nomes de Repositories
- Nomes de DTOs
- Nomes de Enums
- Nomes de arquivos
- Nomes de pastas

Exemplos:

Correto:

Cliente
Agendamento
Aplicador
DataAgendamento
HoraAgendamento
BuscarAgendamentosAsync()
SalvarAgendamentoAsync()

Incorreto:

Customer
Schedule
Provider
AppointmentDate
AppointmentTime
GetSchedulesAsync()
SaveScheduleAsync()

Exceções:

Manter em inglês apenas:

- Frameworks
- Bibliotecas
- Interfaces nativas
- Classes do .NET
- Componentes MAUI
- Dependências externas

Exemplos:

ObservableCollection
ContentPage
CollectionView
HttpClient
DependencyInjection

ARQUITETURA

Sempre utilizar:

/Models
/ViewModels
/Views
/Services
/Repositories
/Validators
/Helpers
/Components
/Resources
/Themes

Nunca colocar regra de negócio dentro das Views.

Toda regra deve ficar em:

- Services
- Validators
- ViewModels

MVVM

Utilizar CommunityToolkit.Mvvm.

Preferir:

[ObservableProperty]
[RelayCommand]

Exemplo:

[ObservableProperty]
private string nomeCliente;

[RelayCommand]
private async Task SalvarAsync()
{
}

Evitar implementação manual de INotifyPropertyChanged.

SERVIÇOS

Sempre criar interface.

Exemplo:

IAgendamentoService

AgendamentoService

Nunca utilizar classes concretas diretamente.

Registrar tudo via Dependency Injection.

ASYNC

Toda operação deve utilizar:

async/await

Nunca utilizar:

Task.Result
Task.Wait()

PERFORMANCE

Priorizar:

- Lazy Loading
- Carregamento assíncrono
- Componentes reutilizáveis
- Virtualização de listas
- ObservableCollection

Evitar:

- Loops desnecessários
- Renderizações excessivas
- Código duplicado

VALIDAÇÕES

Toda entrada deve possuir validação.

Preferencialmente:

FluentValidation ou Validators próprios.

Exemplo:

Nome obrigatório
Data válida
Horário válido
Conflito de agendamento

DESIGN SYSTEM

Todo componente visual deve ser reutilizável.

Criar:

BotaoPrimario
BotaoSecundario
CampoFormulario
CabecalhoPagina
CardInformacao
BadgeStatus
TelaVazia
IndicadorCarregamento

Nunca duplicar XAML.

XAML

Priorizar:

- Grid
- FlexLayout
- CollectionView

Evitar:

- StackLayouts excessivos
- Código repetido

Utilizar DataBinding em tudo.

NAVEGAÇÃO

Utilizar Shell Navigation.

Nunca utilizar navegação acoplada.

Exemplo:

await Shell.Current.GoToAsync(nameof(AgendaPage));

DADOS

Sempre preparar código para futura integração com API REST.

Mesmo usando mocks, criar:

Interfaces
DTOs
Services

para facilitar substituição futura.

LOGS

Preparar estrutura para logging.

Utilizar ILogger sempre que possível.

TESTABILIDADE

Todo código deve ser facilmente testável.

Utilizar:

- Injeção de dependência
- Interfaces
- Serviços desacoplados

NOMENCLATURA

Métodos:

BuscarClienteAsync
SalvarAgendamentoAsync
AtualizarStatusAsync

Variáveis:

nomeCliente
dataAgendamento
horaAplicacao

Coleções:

clientes
agendamentos
aplicadores

Booleanos:

possuiConflito
estaCarregando
usuarioAutenticado

TELAS

Toda tela deve possuir:

View
ViewModel
Service

Exemplo:

AgendaPage
AgendaViewModel
AgendamentoService

PADRÃO VISUAL

Sempre gerar interfaces modernas inspiradas em:

- Microsoft Fluent Design
- Material Design 3
- Aplicativos corporativos premium

Priorizar:

- Espaçamento consistente
- Hierarquia visual clara
- Ícones modernos
- Responsividade
- UX intuitiva

QUALIDADE

Antes de gerar qualquer código, verificar:

- SOLID
- DRY
- KISS
- Clean Code
- Responsabilidade única

Se existir possibilidade de reutilização, criar componente reutilizável ao invés de duplicar código.