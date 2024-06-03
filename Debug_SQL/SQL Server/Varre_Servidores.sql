
-- Quem tem movimento de entrada, NF-e e SAT em abril/2024
SELECT * FROM (
SELECT
     (SELECT SUM(tbMovProdIcms.icms_vIcms)
      FROM tbMovDados
 	         INNER JOIN tbMovProd               WITH(NOLOCK)
	               ON tbMovProd.prod_idMov=tbMovDados.mov_idMov        
	         INNER JOIN tbMovProdIcms           WITH(NOLOCK)
	               ON tbMovProdIcms.icms_idProd=tbMovProd.prod_idProd  
      WHERE  IIF(tbMovDados.mov_dhSaiEnt IS NULL, 
                 tbMovDados.mov_dhEmi, 
                 tbMovDados.mov_dhSaiEnt) BETWEEN '2024-04-01 00:00:00'
                                              AND '2024-04-30 23:59:50'   
	         AND tbMovDados.mov_tpAmb = 1
             AND LEN(tbMovDados.mov_id)>43
	         AND tbMovDados.mov_indStatus = 2 
	         AND tbMovDados.mov_mod IN (55, 59)
			 AND CHARINDEX('Imp',tbMovDados.mov_id) = 0
     ) AS Total_ICMS,
     (SELECT COUNT(*)
      FROM tbMovDados
      WHERE  IIF(tbMovDados.mov_dhSaiEnt IS NULL, 
                 tbMovDados.mov_dhEmi, 
                 tbMovDados.mov_dhSaiEnt) BETWEEN '2024-04-01 00:00:00'
                                              AND '2024-04-30 23:59:50'   
	         AND tbMovDados.mov_tpAmb = 1
             AND LEN(tbMovDados.mov_id)>43
	         AND tbMovDados.mov_indStatus = 2 
	         AND tbMovDados.mov_mod=55
			 AND CHARINDEX('Imp',tbMovDados.mov_id) > 0
     ) AS NFe_entrada,
     (SELECT COUNT(*)
      FROM tbMovDados
      WHERE  IIF(tbMovDados.mov_dhSaiEnt IS NULL, 
                 tbMovDados.mov_dhEmi, 
                 tbMovDados.mov_dhSaiEnt) BETWEEN '2024-04-01 00:00:00'
                                              AND '2024-04-30 23:59:50'   
	         AND tbMovDados.mov_tpAmb = 1
             AND LEN(tbMovDados.mov_id)>43
	         AND tbMovDados.mov_indStatus = 2 
	         AND tbMovDados.mov_mod=55
			 AND CHARINDEX('Imp',tbMovDados.mov_id) = 0
     ) AS NFe_saida,
     (SELECT COUNT(*)
      FROM tbMovDados
      WHERE  IIF(tbMovDados.mov_dhSaiEnt IS NULL, 
                 tbMovDados.mov_dhEmi, 
                 tbMovDados.mov_dhSaiEnt) BETWEEN '2024-04-01 00:00:00'
                                              AND '2024-04-30 23:59:50'   
	         AND tbMovDados.mov_tpAmb = 1
             AND LEN(tbMovDados.mov_id)>43
	         AND tbMovDados.mov_indStatus = 2 
	         AND tbMovDados.mov_mod=59
     ) AS SAT) movto
	 WHERE Total_ICMS > 0 AND NFe_entrada > 0 AND NFe_saida > 0 AND SAT > 0

-------------------------------------------------------------------------------------------------------------------------------------------

-- Quem tem mais de 5.000 registros entre NF-e, NFC-e ou SAT em abril/2024
SELECT * FROM
(SELECT COUNT(*) AS Registros
      FROM tbMovDados
      WHERE  IIF(tbMovDados.mov_dhSaiEnt IS NULL, 
                 tbMovDados.mov_dhEmi, 
                 tbMovDados.mov_dhSaiEnt) BETWEEN '2024-04-01 00:00:00'
                                              AND '2024-04-30 23:59:50'   
	         AND tbMovDados.mov_tpAmb = 1
             AND LEN(tbMovDados.mov_id)>43
	         AND tbMovDados.mov_indStatus = 2 
	         AND tbMovDados.mov_mod IN (55, 59, 65)
) dominios WHERE Registros > 5000

-------------------------------------------------------------------------------------------------------------------------------------------

