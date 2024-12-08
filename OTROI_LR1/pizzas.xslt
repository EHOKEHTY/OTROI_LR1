<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<body>
				<h2>Список піц</h2>
				<table border="1">
					<tr bgcolor="#9acd32">
						<th>Назва</th>
						<th>Ціна</th>
						<th>Інгредієнти</th>
					</tr>
					<xsl:for-each select="Pizzas/Pizza">
						<tr>
							<td>
								<xsl:value-of select="Name"/>
							</td>
							<td>
								<xsl:value-of select="Price"/>
							</td>
							<td>
								<xsl:for-each select="Ingredients/Ingredient">
									<xsl:value-of select="."/>
									<br/>
								</xsl:for-each>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>