CREATE PROCEDURE sp_FinishBudget
    @BudgetId INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (
            SELECT 1
            FROM Orcamento
            WHERE Id = @BudgetId
        )
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT 
                0 AS Success,
                'Budget not found.' AS Message;

            RETURN;
        END;

        IF NOT EXISTS (
            SELECT 1
            FROM Orcamento
            WHERE Id = @BudgetId
              AND Status = 'Opened'
        )
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT 
                0 AS Success,
                'Budget is not open.' AS Message;

            RETURN;
        END;

        IF NOT EXISTS (
            SELECT 1
            FROM OrcamentoItem
            WHERE OrcamentoId = @BudgetId
        )
        BEGIN
            ROLLBACK TRANSACTION;

            SELECT 
                0 AS Success,
                'Budget has no items.' AS Message;

            RETURN;
        END;

        DECLARE @TotalAmount DECIMAL(18, 2);

        UPDATE OrcamentoItem
        SET ValorTotal = Quantidade * ValorUnitario
        WHERE OrcamentoId = @BudgetId;

        SELECT 
            @TotalAmount = SUM(ValorTotal)
        FROM OrcamentoItem
        WHERE OrcamentoId = @BudgetId;

        UPDATE Orcamento
        SET 
            ValorTotal = @TotalAmount,
            Status = 'Finished',
            DataFinalizacao = GETDATE()
        WHERE Id = @BudgetId;

        COMMIT TRANSACTION;

        SELECT 
            1 AS Success,
            'Budget finalized successfully.' AS Message,
            @TotalAmount AS TotalAmount;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SELECT 
            0 AS Success,
            ERROR_MESSAGE() AS Message;
    END CATCH;
END;