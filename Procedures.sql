CREATE OR ALTER PROCEDURE SP_GET_MESSAGES
    @UserId INT,
    @SearchType INT = 3,
    @WhereClause NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IncomingCondition NVARCHAR(MAX) = 'M.ID IN (
        SELECT MESSAGE_ID FROM INCOMING_MESSAGES WHERE RECEIVER_ID = @UserId
        UNION
        SELECT MESSAGE_ID FROM INNER_MESSAGES WHERE RECEIVER_ID = @UserId
    )';

    DECLARE @OutgoingCondition NVARCHAR(MAX) = 'M.ID IN (
        SELECT MESSAGE_ID FROM OUTGOING_MESSAGES WHERE SENDER_ID = @UserId
        UNION
        SELECT MESSAGE_ID FROM INNER_MESSAGES WHERE SENDER_ID = @UserId
    )';

    DECLARE @CombinedCondition NVARCHAR(MAX);

    IF @SearchType = 1
        SET @CombinedCondition = @IncomingCondition;
    ELSE IF @SearchType = 2
        SET @CombinedCondition = @OutgoingCondition;
    ELSE 
        SET @CombinedCondition = '(' + @IncomingCondition + ' OR ' + @OutgoingCondition + ')';

    DECLARE @FinalWhere NVARCHAR(MAX) = 
	CASE 
         WHEN @WhereClause IS NOT NULL AND LTRIM(RTRIM(@WhereClause)) <> '' 
         THEN 'AND ' + @WhereClause 
         ELSE '' 
     END;

    DECLARE @Sql NVARCHAR(MAX) = '
    WITH SenderCandidates AS
    (
        SELECT M.ID AS MessageId,
               US_OUT.EMAIL AS OutgoingSender,
               US_IN.EMAIL AS InnerSender,
               INCM.SENDER_EMAIL AS IncomingSender
        FROM MESSAGES M
        LEFT JOIN OUTGOING_MESSAGES OMSG ON OMSG.MESSAGE_ID = M.ID
        LEFT JOIN USERS US_OUT ON OMSG.SENDER_ID = US_OUT.ID

        LEFT JOIN INNER_MESSAGES IM ON IM.MESSAGE_ID = M.ID
        LEFT JOIN USERS US_IN ON IM.SENDER_ID = US_IN.ID

        LEFT JOIN INCOMING_MESSAGES INCM ON INCM.MESSAGE_ID = M.ID
    ),
    SenderInfo AS
    (
        SELECT MessageId,
               COALESCE(MAX(OutgoingSender), MAX(InnerSender), MAX(IncomingSender)) AS SenderEmail
        FROM SenderCandidates
        GROUP BY MessageId
    ),
    ReceiverInfo AS
    (
        SELECT 
            MSG_ID,
            STRING_AGG(ReceiverEmail, '';'') AS ReceiverEmails
        FROM
        (
            SELECT DISTINCT IM.MESSAGE_ID AS MSG_ID, U.EMAIL AS ReceiverEmail
            FROM INCOMING_MESSAGES IM
            JOIN USERS U ON IM.RECEIVER_ID = U.ID

            UNION

            SELECT DISTINCT OMSG.MESSAGE_ID AS MSG_ID, OMSG.RECEIVER_EMAIL AS ReceiverEmail
            FROM OUTGOING_MESSAGES OMSG

            UNION

            SELECT DISTINCT IM2.MESSAGE_ID AS MSG_ID, U2.EMAIL AS ReceiverEmail
            FROM INNER_MESSAGES IM2
            JOIN USERS U2 ON IM2.RECEIVER_ID = U2.ID
        ) R
        GROUP BY MSG_ID
    )

    SELECT 
        M.ID AS MessageId,
        M.SUBJECT AS Subject,
        M.CONTENT AS Content,
        SI.SenderEmail AS SenderEmail,
        RI.ReceiverEmails AS ReceiverEmails,
        M.DATE_OF_COMPLETION AS DateOfCompletion,
        M.STATUS AS Status
    FROM MESSAGES M
    JOIN SenderInfo SI ON M.ID = SI.MessageId
    LEFT JOIN ReceiverInfo RI ON M.ID = RI.MSG_ID
    WHERE ' + @CombinedCondition + '
    ' + @FinalWhere + '
    ORDER BY M.DATE_OF_COMPLETION DESC;';

    EXEC sp_executesql @Sql, N'@UserId INT', @UserId;
END
GO
