<?xml version="1.0" encoding="UTF-8"?>
<?altova_samplexml file:///D:/brekken/discovery/transformations/http/hal+json/001-input.xml?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<body>
				<xsl:apply-templates select="dictionary"/>
			</body>
		</html>
	</xsl:template>
	<xsl:template match="dictionary">
		<h1>Properties</h1>
		<div>
			<xsl:apply-templates select="string"/>
			<xsl:apply-templates select="list"/>
		</div>
	</xsl:template>
	<xsl:template match="list">
		<h1>Links</h1>
		<div>
			<xsl:apply-templates select="string"/>
		</div>
	</xsl:template>
	<xsl:template match="string">
		<div>
			<xsl:choose>
				<xsl:when test="@action">
					<a href="{text()}" title="{@prompt} {text()}">
						<xsl:choose>
							<xsl:when test="@prompt!=''">
								<xsl:value-of select="@prompt"/>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="@relation"/>
							</xsl:otherwise>
						</xsl:choose>
					</a>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="text()"/>
				</xsl:otherwise>
			</xsl:choose>
		</div>
	</xsl:template>
</xsl:stylesheet>
