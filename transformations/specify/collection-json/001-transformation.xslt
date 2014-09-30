<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<!-- XML output -->
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
	
	<!-- Root -->
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>

	<!-- collection -->
	<xsl:template match="collection">
		<document>
			<dictionary name="{name(.)}">
				<string name="version"><xsl:value-of select="version"/></string>
				<string name="href" action="read" transclude="false" relation="self" prompt="Refresh"><xsl:value-of select="href"/></string>
				<xsl:apply-templates select="links_list"/>
				<xsl:apply-templates select="items_list"/>
				<xsl:apply-templates select="queries_list"/>
				<xsl:apply-templates select="template">
					<xsl:with-param name="action" select="'append'"/>
					<xsl:with-param name="relation" select="'create'"/>
					<xsl:with-param name="prompt" select="'Add item'"/>
					<xsl:with-param name="iri" select="href"/>
				</xsl:apply-templates>
				<dictionary name="error"/>
			</dictionary>
		</document>
	</xsl:template>

	<!-- template -->
	<xsl:template match="template">
		<xsl:param name="action"/>
		<xsl:param name="relation"/>
		<xsl:param name="prompt"/>
		<xsl:param name="iri"/>
		<dictionary name="template" action="{$action}" transclude="false" relation="{$relation}" prompt="{$prompt}" iri="{$iri}">
			<xsl:apply-templates select="data_list"/>
		</dictionary>
	</xsl:template>

	<!-- links list -->
	<xsl:template match="links_list">
		<list name="links">
			<xsl:apply-templates select="links" />
		</list>
	</xsl:template>

	<!-- links element -->
	<xsl:template match="links">
		<string name="{./name}" action="read" transclude="false" relation="{./rel}" prompt="{./prompt}"><xsl:value-of select="href"/></string>
	</xsl:template>

	<!-- items list -->
	<xsl:template match="items_list">
		<list name="{name(.)}">
			<xsl:apply-templates select="items"/>
		</list>
	</xsl:template>
	
	<!-- items element -->
	<xsl:template match="items">
		<dictionary name="{name(.)}">
			<string action="read" transclude="false" relation="item" prompt="Open"><xsl:value-of select="href"/></string>
			<xsl:apply-templates select="data_list"/>
			<xsl:apply-templates select="links_list"/>
			<xsl:apply-templates select="template">
				<xsl:with-param name="action" select="'replace'"/>
				<xsl:with-param name="relation" select="'edit'"/>
				<xsl:with-param name="prompt" select="'Edit item'"/>
				<xsl:with-param name="iri" select="href"/>
			</xsl:apply-templates>
			<string action="remove" transclude="false" relation="delete" prompt="Delete"><xsl:value-of select="href"/></string>
		</dictionary>
	</xsl:template>
	
	<!-- data list -->
	<xsl:template match="data_list">
		<xsl:apply-templates select="data"/>
	</xsl:template>
	
	<!-- data element -->
	<xsl:template match="data">
		<string name="{./name}" prompt="{./prompt}"><xsl:value-of select="value"/></string>
	</xsl:template>
	
	<!-- queries list -->
	<xsl:template match="queries_list">
		<list name="{name(.)}">
			<xsl:apply-templates select="queries"/>
		</list>
	</xsl:template>
	
	<!-- queries element -->
	<xsl:template match="queries">
		<dictionary name="{./name}" action="read" transclude="false" relation="{./rel}" prompt="{./prompt}" iri="{./href}">
			<xsl:apply-templates select="data_list"/>
		</dictionary>
	</xsl:template>
</xsl:stylesheet>
