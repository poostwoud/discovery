<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:specify="http://github.com/poostwoud/discovery/specify">
	<!-- XML output -->
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
	
	<!-- Curies (TODO) -->
	<!-- http://www.xml.com/pub/a/2003/09/03/trxml.html -->
	<!--xsl:variable name="curies" select="document/_links/curies"/>
	<xsl:function name="specify:apply-curies">
		<xsl:param name="curied"/>
		<xsl:value-of select="$curies"/>
		<xsl:value-of select="$curied"/>
	</xsl:function-->
	
	<!-- Root -->
	<xsl:template match="/">
		<xsl:apply-templates select="document"/>
	</xsl:template>
	
	<!-- document -->
	<xsl:template match="document">
		<dictionary name="root">
			<!-- links -->
			<xsl:apply-templates select="_links"/>
			<!-- embedded -->
			<xsl:apply-templates select="_embedded"/>
			<!-- properties -->
			<!-- TODO:Use of apply templates possible? -->
			<xsl:call-template name="properties">
				<xsl:with-param name="this" select="*[not(self::_links) and not(self::_embedded)]"/>
			</xsl:call-template>
		</dictionary>
	</xsl:template>
	
	<!-- _links (ignore curies) -->
	<xsl:template match="_links">
		<list name="_links">
			<xsl:apply-templates select="*[not(self::curies)]"/>
		</list>
	</xsl:template>
	
	<xsl:template match="_links/*">
		<string name="" action="read" transclude="false" relation="{name(.)}"><xsl:value-of select="href"/></string>
	</xsl:template>
	
	<!-- _embedded -->
	<xsl:template match="_embedded">
		<list name="_embedded">
			<xsl:apply-templates select="*"/>
		</list>
	</xsl:template>
	
	<xsl:template match="_embedded/*">
		<dictionary name="{name(.)}">
			<!-- links -->
			<xsl:apply-templates select="_links"/>
			<!-- embedded -->
			<xsl:apply-templates select="_embedded"/>
			<!-- properties -->
			<xsl:call-template name="properties">
				<xsl:with-param name="this" select="*[not(self::_links)]"/>
			</xsl:call-template>
		</dictionary>
	</xsl:template>
	
	<!-- properties -->
	<xsl:template name="properties">
		<xsl:param name="this"/>
		<xsl:for-each select="$this">
			<string name="{name()}"><xsl:value-of select="text()"/></string>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>
