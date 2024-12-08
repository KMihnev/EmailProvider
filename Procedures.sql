CREATE OR ALTER PROCEDURE SP_GET_MESSAGES
    @UserId INT,
    @SearchType INT = 3
AS
BEGIN
    CREATE TABLE #CombinedMessages (
        MessageId INT,
        Subject NVARCHAR(128),
        Content NVARCHAR(896),
        SenderEmail NVARCHAR(64),
        ReceiverEmails NVARCHAR(MAX),
        DateOfCompletion DATETIME,
        Status INT
    );

    IF @SearchType = 1 OR @SearchType = 3
    BEGIN
        INSERT INTO #CombinedMessages
        SELECT 
            M.ID AS MessageId,
            M.Subject,
            M.Content,
            U.EMAIL AS SenderEmail,
            STRING_AGG(OM.RECEIVER_EMAIL, ';') + 
            CASE 
                WHEN EXISTS (SELECT 1 FROM INNER_MESSAGES IM WHERE IM.MESSAGE_ID = M.ID AND IM.SENDER_ID = @UserId) 
                THEN ';' + STRING_AGG(R.EMAIL, ';') 
                ELSE '' 
            END AS ReceiverEmails,
            M.DATE_OF_COMPLETION,
            M.Status
        FROM MESSAGES M
        LEFT JOIN OUTGOING_MESSAGES OM ON OM.MESSAGE_ID = M.ID AND OM.SENDER_ID = @UserId
        LEFT JOIN INNER_MESSAGES IM ON IM.MESSAGE_ID = M.ID AND IM.SENDER_ID = @UserId
        LEFT JOIN USERS U ON OM.SENDER_ID = U.ID
        LEFT JOIN USERS R ON IM.RECEIVER_ID = R.ID
        WHERE OM.SENDER_ID = @UserId OR IM.SENDER_ID = @UserId
        GROUP BY M.ID, M.Subject, M.Content, U.EMAIL, M.DATE_OF_COMPLETION, M.Status;
    END;

    IF @SearchType = 2 OR @SearchType = 3
    BEGIN
        INSERT INTO #CombinedMessages
        SELECT 
            M.ID AS MessageId,
            M.Subject,
            M.Content,
            IM.SENDER_EMAIL AS SenderEmail,
            STRING_AGG(U.EMAIL, ';') AS ReceiverEmails,
            M.DATE_OF_COMPLETION,
            M.Status
        FROM INCOMING_MESSAGES IM
        INNER JOIN MESSAGES M ON IM.MESSAGE_ID = M.ID
        INNER JOIN USERS U ON IM.RECEIVER_ID = U.ID
        WHERE IM.RECEIVER_ID = @UserId
        GROUP BY M.ID, M.Subject, M.Content, IM.SENDER_EMAIL, M.DATE_OF_COMPLETION, M.Status;

        INSERT INTO #CombinedMessages
        SELECT 
            M.ID AS MessageId,
            M.Subject,
            M.Content,
            S.EMAIL AS SenderEmail,
            STRING_AGG(R.EMAIL, ';') AS ReceiverEmails,
            M.DATE_OF_COMPLETION,
            M.Status
        FROM INNER_MESSAGES IM
        INNER JOIN MESSAGES M ON IM.MESSAGE_ID = M.ID
        INNER JOIN USERS S ON IM.SENDER_ID = S.ID
        INNER JOIN USERS R ON IM.RECEIVER_ID = R.ID
        WHERE IM.RECEIVER_ID = @UserId
        GROUP BY M.ID, M.Subject, M.Content, S.EMAIL, M.DATE_OF_COMPLETION, M.Status;
    END;

    SELECT * FROM #CombinedMessages;

    DROP TABLE #CombinedMessages;
END;
GO