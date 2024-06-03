--USE [dbsaurus_mariatereza1]
USE [dbsaurus_testessped]
GO
/****** Object:  UserDefinedFunction [dbo].[ff_teste]    Script Date: 02/04/2024 09:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[ff_teste]
(		
    @teste INT
) 
RETURNS @tbRetorno TABLE (nroLinha INT, descLinha NVARCHAR(2000))
BEGIN

/*

    IMPORTANTE!!! -> Esta FUNCTION está chamando  [dbo].[ff_txtSPEDIcms2]
	=====================================================================


	Para criar e alimentar a tabela de correlação entre o CFOP de saída do fornecedor e de entrada no estoque para o SPED, registros C170 e C190

	     * SQLQuery_IncluirTabCfopsEntr.sql

*/

DECLARE	@dhIniSPED DATETIME = '2024-03-01 01:01:01.001'
DECLARE	@dhFimSPED DATETIME = '2024-03-31 23:50:01.001'
DECLARE @idLoja INT = 1
DECLARE @idTpCodigo INT = 0  
DECLARE @tpAmb INT = 1

DECLARE @xmlTeste XML = '<parametrosV4>
                             <versaoSped>004</versaoSped>
						     <registro1600>S</registro1600>
					         <registro1601>S</registro1601>
                         </parametrosV4>'

						 --SET @xmlTeste = '<parametrosV4>
					  --                       <registro1600>N</registro1600>
				   --                          <registro1601>N</registro1601>
       --                                   </parametrosV4>'

INSERT INTO @tbRetorno
SELECT * FROM [dbo].[ff_txtSPEDIcms2] (
   @dhIniSPED,
   @dhFimSPED, 
   @idLoja, 
   @idTpCodigo,
   @tpAmb,
   @xmlTeste
)

RETURN

END
