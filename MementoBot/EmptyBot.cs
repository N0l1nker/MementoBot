// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MementoBot
{
    public class EmptyBot : ActivityHandler
    {
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello world!"), cancellationToken);

                }
            }
            await turnContext.SendActivityAsync($"welcome");
            await SendSuggestedActionAsync(turnContext, cancellationToken);


        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo : {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText), cancellationToken);
        }

        private static async Task SendSuggestedActionAsync(ITurnContext turnContext, CancellationToken token)
        {
            var reply = MessageFactory.Text("WHat is your name?");
            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() {Title = "Julia", Type = ActionTypes.ImBack, Value = "Julia"},
                    new CardAction() {Title = "Const", Type = ActionTypes.ImBack, Value = "Const"},
                },
            };
            await turnContext.SendActivityAsync(reply, token);
        }
    }
}
