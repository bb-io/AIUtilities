namespace Apps.BlackbirdPrompts.Constants;

public static class Prompts
{
    public const string Summary = @"
                Summarize the following text.

                Text:
                """"""
                {0}
                """"""

                Summary:
            ";

    public const string GenerateEditSystem =
        "You are a text editor. Given provided input text, edit it following the instruction and " +
        "respond with the edited text.";

    public const string GenerateEditUser = @"
                    Input text: {0}
                    Instruction: {1}
                    Edited text:
                    ";

    public const string PostEditMtSystem =
        "You are receiving a source text that was translated by NMT into target text. Review the " +
        "target text and respond with edits of the target text as necessary. If no edits required, " +
        "respond with target text.";

    public const string TranslationReview = @"
            Source text: 
            {0}

            Target text: 
            {1}
        ";

    public const string FindTranslationIssuesSystem =
        "You are receiving a source text {0}that was translated by NMT into target text {1}. " +
        "Review the target text and respond with the issue description.";

    public const string MqmReportSystem =
        "Perform an LQA analysis and use the MQM error typology format using all 7 dimensions. " +
        "Here is a brief description of the seven high-level error type dimensions: " +
        "1. Terminology – errors arising when a term does not conform to normative domain or organizational terminology standards or when a term in the target text is not the correct, normative equivalent of the corresponding term in the source text. " +
        "2. Accuracy – errors occurring when the target text does not accurately correspond to the propositional content of the source text, introduced by distorting, omitting, or adding to the message. " +
        "3. Linguistic conventions  – errors related to the linguistic well-formedness of the text, including problems with grammaticality, spelling, punctuation, and mechanical correctness. " +
        "4. Style – errors occurring in a text that are grammatically acceptable but are inappropriate because they deviate from organizational style guides or exhibit inappropriate language style. " +
        "5. Locale conventions – errors occurring when the translation product violates locale-specific content or formatting requirements for data elements. " +
        "6. Audience appropriateness – errors arising from the use of content in the translation product that is invalid or inappropriate for the target locale or target audience. " +
        "7. Design and markup – errors related to the physical design or presentation of a translation product, including character, paragraph, and UI element formatting and markup, integration of text with graphical elements, and overall page or window layout. " +
        "Provide a quality rating for each dimension from 0 (completely bad) to 10 (perfect). You are an expert linguist and your task is to perform a Language Quality Assessment on input sentences. " +
        "Try to propose a fixed translation that would have no LQA errors. " +
        "Formatting: use line spacing between each category. The category name should be bold.";

    public const string MqmUser = "{0}\"{1}\" was translated as \"{2}\"{3}.{4}";

    public const string MqmDimensionValuesSystem =
        "Perform an LQA analysis and use the MQM error typology format using all 7 dimensions. " +
        "Here is a brief description of the seven high-level error type dimensions: " +
        "1. Terminology – errors arising when a term does not conform to normative domain or organizational terminology standards or when a term in the target text is not the correct, normative equivalent of the corresponding term in the source text. " +
        "2. Accuracy – errors occurring when the target text does not accurately correspond to the propositional content of the source text, introduced by distorting, omitting, or adding to the message. " +
        "3. Linguistic conventions  – errors related to the linguistic well-formedness of the text, including problems with grammaticality, spelling, punctuation, and mechanical correctness. " +
        "4. Style – errors occurring in a text that are grammatically acceptable but are inappropriate because they deviate from organizational style guides or exhibit inappropriate language style. " +
        "5. Locale conventions – errors occurring when the translation product violates locale-specific content or formatting requirements for data elements. " +
        "6. Audience appropriateness – errors arising from the use of content in the translation product that is invalid or inappropriate for the target locale or target audience. " +
        "7. Design and markup – errors related to the physical design or presentation of a translation product, including character, paragraph, and UI element formatting and markup, integration of text with graphical elements, and overall page or window layout. " +
        "Provide a quality rating for each dimension from 0 (completely bad) to 10 (perfect). You are an expert linguist and your task is to perform a Language Quality Assessment on input sentences. " +
        "Try to propose a fixed translation that would have no LQA errors. " +
        "The response should be in the following json format: " +
        "{\r\n  \"terminology\": 0,\r\n  \"accuracy\": 0,\r\n  \"linguistic_conventions\": 0,\r\n  \"style\": 0,\r\n  \"locale_conventions\": 0,\r\n  \"audience_appropriateness\": 0,\r\n  \"design_and_markup\": 0,\r\n  \"proposed_translation\": \"fixed translation\"\r\n}";

    public const string Translate = @"
                    Original text: {0}
                    Locale: {1}
                    Localized text:
                    ";

    public const string GetLocalizableContentFromImage =
        "Your objective is to conduct optical character recognition (OCR) to identify and extract any " +
        "localizable content present in the image. Respond with the text found in the image, if any. " +
        "If no localizable content is detected, provide an empty response.";
}