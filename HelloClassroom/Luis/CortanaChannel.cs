using Newtonsoft.Json;

namespace CortanaChannel
{
    public class CortanaChannelData
    {/// <summary>
     /// Gets or sets the Speech output response from the web service
     /// </summary>
        [JsonProperty("speechOutput")]
        public SpeechOutput SpeechOutput { get; set; }

        /// <summary>
        /// Gets or sets the Speech output response from the web service
        /// </summary>
        [JsonProperty("reprompt")]
        public SpeechOutput Reprompt { get; set; }

        /// <summary>
        /// Gets or sets the state of the conversation as seen by the bot.
        /// </summary>
        [JsonProperty("botState")]
        public BotConversationState BotState { get; set; }

        /// <summary>
        /// Gets or sets the data attached to the end-of-conversation bot state.
        /// </summary>
        [JsonProperty("endOfConversation")]
        public EndOfConversationData EndOfConversation { get; set; }

    }

    public class EndOfConversationData
    {
        /// <summary>
        /// Gets or sets the reason for ending the conversation
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }

    public class CompletionReasonTypes
    {
        public const string CompletedSuccessfully = "completedSuccessfully";
        public const string UserCanceled = "userCanceled";
        public const string BotTimedOut = "botTimedOut";
        public const string BotIssuedInvalidMessage = "botIssuedInvalidMessage";
        public const string ChannelFailed = "channelFailed";
        public const string Unknown = "unknown";
    }

    public class SpeechOutput
    {
        private string ssml;

        /// <summary>
        /// Gets or sets text to be spoken
        /// </summary>
        [JsonProperty("speaktext")]
        public string SpeakText
        {
            get { return ssml; }
            set {
                ssml = SSMLDecorator.Decorate(value);
            }
        }

        /// <summary>
        /// Gets or sets the text to be displayed when the corresponding spoken text is being read aloud.
        /// </summary>
        [JsonProperty("displayText")]
        public string DisplayText { get; set; }
    }
    public enum BotConversationState
    {
        /// <summary> 
        /// This message is something the bot would like to say/show/do. Bot has nothing more to  
        /// say but the user is welcome to follow-up with a statement or question and the bot  
        /// will handle that as part of this conversation. 
        /// </summary> 
        FinishedButUserMayReEngage,

        /// <summary> 
        /// This message is something the bot would like to show/do but it's not done talking yet  
        /// and will say something else soon in a separate message, without input from the user. 
        /// </summary> 
        HasMoreToSay,

        /// <summary> 
        /// This message is a question and bot will wait for a response and take no further  
        /// action. 
        /// </summary> 
        WaitingForAnswerToQuestion,

        /// <summary> 
        /// This message is something the bot would like to say/show/do. It has nothing more  
        /// to say and is finished with this conversation. User must initiate a new  
        /// conversation to chat further. 
        /// </summary> 
        FinishedWithConversation
    }



    /// <summary>
    /// A class to help decorate a plain text data with SSML format
    /// </summary>
    internal static class SSMLDecorator
    {
        /// <summary>
        /// Prefix of SSML format
        /// </summary>
        static string pre = @"<speak version = '1.0' xmlns='http://www.w3.org/2001/10/synthesis' " +
                                        "xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' " +
                                        "xsi:schemaLocation='http://www.w3.org/2001/10/synthesis " +
                                        "http://www.w3.org/TR/speech-synthesis/synthesis.xsd' ";

        static string suf = @"</speak>";
        static string Locale = "EN-US";

        /// <summary>
        /// Decorate a plain text data with SSML format
        /// </summary>
        /// <param name="ttsData">Text data to be decorated</param>
        /// <returns>The text data in SSML format</returns>
        public static string Decorate(string ttsData)
        {
            if(ttsData == null) {
                ttsData = string.Empty;
            }

            if(!ttsData.Contains("</speak>")) {
                var language = "xml:lang='EN-US'>";
                ttsData = pre + language + ttsData + suf;
            }

            return ttsData;
        }
    }
}