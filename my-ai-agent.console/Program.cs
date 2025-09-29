using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using MyAiAgent.Console.Plugins;

const string modelId = "gpt-5-mini";
const string endpoint = "https://evillegas-openai-dev.openai.azure.com/";
const string apiKey = "";

// Create a kernel with Azure OpenAI chat completion
IKernelBuilder kernelBuilder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Add enterprise components
kernelBuilder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

// Build the kernel
Kernel kernel = kernelBuilder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Add a plugin (the LightsPlugin class is an example)
kernel.Plugins.AddFromType<LightsPlugin>("Lights");

// Enable planning (automatic function calling)
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

// Create a history store the conversation
var history = new ChatHistory();

// Initiate a back-and-forth chat
string? userInput;
do
{
    Console.WriteLine("User > ");
    userInput = Console.ReadLine();
    history.AddUserMessage(userInput);
    // Get a response from the AI
    ChatMessageContent aiResponse = await chatCompletionService.GetChatMessageContentAsync(history, executionSettings: openAIPromptExecutionSettings, kernel: kernel);
    Console.WriteLine("AI > " + aiResponse);

    // Add the message from the agent to the chat history
    history.AddMessage(aiResponse.Role, aiResponse.Content ?? string.Empty);
} while (!string.IsNullOrWhiteSpace(userInput));
