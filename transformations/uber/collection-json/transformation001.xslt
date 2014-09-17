<?xml version="1.0" encoding="UTF-8"?>
<?altova_samplexml file:///D:/brekken/Collection+JSON.example001.xml?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions" exclude-result-prefixes="xs fn">
	<!-- XML output -->
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

	<!-- Helper variables -->
    <xsl:variable name="newline" select="'&#x0A;'" />
    <xsl:variable name="tab" select="'&#x09;'" />

	<!-- UBER::Format Documentation::https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_the_tt_lt_uber_gt_tt_element::3.6. The uber element -->
	<xsl:template match="/">
		<uber version="1.0">
			<xsl:apply-templates/>
		</uber>	
	</xsl:template>
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#objects::3.1. collection -->
	<xsl:template match="collection">
		<!-- UBER::Format Documentation::https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_xml_example::5.1. XML Example -->
		<xsl:if test="href">
			<data rel="self" url="{href}"/>
		</xsl:if>
		
		<xsl:element name="data">
			<xsl:attribute name="id"><xsl:value-of select="name(.)"/></xsl:attribute>
			<xsl:attribute name="name"><xsl:value-of select="name(.)"/></xsl:attribute>
			<xsl:attribute name="rel">collection</xsl:attribute>
			<!-- href; element:SHOULD -->
			<xsl:apply-templates select="href" />
			<!-- version; element:SHOULD -->
			<xsl:apply-templates select="version"/>
			<!-- links; element:MAY -->
			<xsl:apply-templates select="links"/>
			<!-- items; element:MAY -->
			<xsl:apply-templates select="items"/>
			<!-- queries; element:MAY -->
			<!-- Format Documentation:2.2. Query Templates -->
			<xsl:apply-templates select="queries"/>			
			<!-- template; element:MAY -->
			<!-- Format Documentation:2.1.2. Adding an item -->
			<xsl:if test="href and template">
				<xsl:call-template name="template">
					<xsl:with-param name="template" select="template"/>
					<xsl:with-param name="rel" select="'create'"/>
					<xsl:with-param name="href" select="href"/>
					<xsl:with-param name="action" select="'append'"/>
					<xsl:with-param name="prompt" select="'Add item'"/>
				</xsl:call-template>
			</xsl:if>
			<!-- error; element:MAY -->
			<xsl:apply-templates select="error"/>
		</xsl:element>
	</xsl:template>
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#objects::3.2. error -->
	<xsl:template match="error">
		<error>
			<!-- title; element:MAY -->
			<xsl:apply-templates select="title"/>
			<!-- code; element:MAY -->
			<xsl:apply-templates select="code"/>
			<!-- message; element:MAY -->
			<xsl:apply-templates select="message"/>
		</error>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#objects::3.3. template -->
	<xsl:template name="template">
		<xsl:param name="template"/>
		<xsl:param name="rel"/>
		<xsl:param name="href"/>
		<xsl:param name="action"/>		
		<xsl:param name="prompt"/>
		
		<xsl:element name="data">
			<xsl:attribute name="name"><xsl:value-of select="name($template)"/></xsl:attribute>
			<xsl:attribute name="rel"><xsl:value-of select="$rel"/></xsl:attribute>
			<xsl:attribute name="url"><xsl:value-of select="$href"/></xsl:attribute>
			
			<!-- data; element:OPTIONAL -->
			<!-- data; model -->
			<xsl:if test="$template/data">
				<!-- raw model output -->
				<xsl:variable name="rawmodel">
					<xsl:call-template name="data-action">
						<xsl:with-param name="data" select="$template/data"/>
					</xsl:call-template>
				</xsl:variable>
				<!-- strip newlines and tabs (TODO:Refactor) -->
				<xsl:variable name="model" select="translate(translate($rawmodel, $newline, ''), $tab, '')"/>
				<!-- model attribute -->
				<xsl:attribute name="model">
					<xsl:value-of select="$model"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:attribute name="action"><xsl:value-of select="$action"/></xsl:attribute>

			<!-- prompt; element:OPTIONAL -->
			<xsl:if test="$prompt">
				<data name="prompt"><xsl:value-of select="$prompt"/></data>
			</xsl:if>
			<!-- data; "form" -->			
			<xsl:apply-templates select="$template/data"/>			
		</xsl:element>
	</xsl:template>		
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#arrays::4.1. items -->
	<xsl:template match="items">
		<xsl:element name="data">
			<xsl:attribute name="name"><xsl:value-of select="name(.)"/></xsl:attribute>
			<xsl:attribute name="rel">item</xsl:attribute>
			<!-- href; element:SHOULD -->
			<xsl:apply-templates select="href" />
			<!-- data; element:MAY -->
			<xsl:apply-templates select="data"/>
			<!-- links; element:MAY -->
			<xsl:apply-templates select="links"/>
			<!-- Format Documentation:2.1.4. Updating an item -->
			<xsl:variable name="template" select="parent::node()/template"/>
			<xsl:if test="href and $template">
				<xsl:call-template name="template">
					<xsl:with-param name="template" select="$template"/>
					<xsl:with-param name="rel" select="'edit'"/>
					<xsl:with-param name="href" select="href"/>
					<xsl:with-param name="prompt" select="'Update item'"/>
				</xsl:call-template>
			</xsl:if>
			
			<!-- Format Documentation:2.1.5. Deleting an item -->
			<xsl:if test="href">
				<data rel="delete" url="{href}" action="remove">
					<data name="prompt">Delete item</data>
				</data>
			</xsl:if>			
		</xsl:element>
	</xsl:template>
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#arrays::4.2. data -->
	<xsl:template match="data">
		<xsl:element name="data">
			<xsl:attribute name="name"><xsl:value-of select="name(.)"/></xsl:attribute>
			
			<!-- name; element:REQUIRED -->
			<!-- value; element:OPTIONAL -->
			<xsl:call-template name="data-name-value">
				<xsl:with-param name="name" select="name"/>
				<xsl:with-param name="value" select="value"/>
			</xsl:call-template>
			
			<!-- prompt; element:OPTIONAL -->
			<xsl:apply-templates select="prompt"/>
		</xsl:element>
	</xsl:template>
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#arrays::4.3. queries -->
	<xsl:template match="queries">
		<xsl:element name="data">
			<xsl:attribute name="name"><xsl:value-of select="name(.)"/></xsl:attribute>
			<!-- rel; element:REQUIRED -->
			<xsl:attribute name="rel"><xsl:value-of select="rel"/></xsl:attribute>
			<!-- href; element:REQUIRED -->
			<xsl:attribute name="url"><xsl:value-of select="href"/></xsl:attribute>
			<!-- data; element:OPTIONAL -->
			<!-- data; model -->
			<xsl:if test="data">
				<!-- raw model output -->
				<xsl:variable name="rawmodel">
					<xsl:call-template name="data-action">
						<xsl:with-param name="data" select="data"/>
					</xsl:call-template>
				</xsl:variable>
				<!-- strip newlines and tabs (TODO:Refactor) -->
				<xsl:variable name="model" select="translate(translate($rawmodel, $newline, ''), $tab, '')"/>
				<!-- model attribute -->
				<xsl:attribute name="model">
					<xsl:value-of select="$model"/>
				</xsl:attribute>
			</xsl:if>
			<!-- name; element:OPTIONAL -->
			<xsl:apply-templates select="name"/>
			<!-- prompt; element:OPTIONAL -->
			<xsl:apply-templates select="prompt"/>
			<!-- data; "form" -->			
			<xsl:apply-templates select="data"/>
		</xsl:element>
	</xsl:template>
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#arrays::4.4. links -->
	<xsl:template match="links">
		<xsl:element name="data">
			<xsl:attribute name="name"><xsl:value-of select="name(.)"/></xsl:attribute>
			<!-- rel; element:REQUIRED -->
			<xsl:apply-templates select="rel" />
			<!-- href; element:REQUIRED -->
			<xsl:apply-templates select="href" />
			<!-- render; element:OPTIONAL -->
			<xsl:apply-templates select="render" />
			<!-- name; element:OPTIONAL -->
			<xsl:apply-templates select="name" />
			<!-- prompt; element:OPTIONAL -->
			<xsl:apply-templates select="prompt" />
		</xsl:element>
	</xsl:template>	
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.1. code -->
	<xsl:template match="code">
		<data name="code"><xsl:value-of select="."/></data>
	</xsl:template>	
	
	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.2. href -->
	<xsl:template match="href">
		<xsl:attribute name="url"><xsl:value-of select="."/></xsl:attribute>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.3. message -->
	<xsl:template match="message">
		<data name="message"><xsl:value-of select="."/></data>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.4. name -->
	<xsl:template match="name">
		<data name="{.}"><xsl:value-of select="."/></data>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.5. prompt -->
	<xsl:template match="prompt">
		<data name="prompt"><xsl:value-of select="."/></data>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.6. rel -->
	<xsl:template match="rel">
		<xsl:attribute name="rel"><xsl:value-of select="."/></xsl:attribute>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.7. render -->
	<xsl:template match="render">
		<!-- render=image = transclude=true -->
		<xsl:if test=". = 'image'">
			<xsl:attribute name="transclude">true</xsl:attribute>
		</xsl:if>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.8. title -->
	<xsl:template match="title">
		<data name="title"><xsl:value-of select="."/></data>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.9. value -->
	<xsl:template match="value">
		<xsl:value-of select="."/>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#properties::5.10. version -->
	<xsl:template match="version">
		<data name="version" rel="latest-version">1.0</data>
	</xsl:template>

	<!-- CJ::Format Documentation::http://amundsen.com/media-types/collection/format/#arrays::4.2. data -->
	<!-- UBER::Format Documentation::https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_supporting_uber_documents_over_http::4.1.2. Using UBER model Values to create HTTP Query Strings -->
	<!-- UBER::Format Documentation::https://rawgit.com/mamund/media-types/master/uber-hypermedia.html#_supporting_uber_documents_over_http::4.1.3. Using UBER model Values to create HTTP Request Bodies -->
	<xsl:template name="data-action">
		<xsl:param name="data"/> 
		<xsl:for-each select="$data">
			<xsl:choose>
				<xsl:when test="position() = 1">?</xsl:when>
				<xsl:otherwise><![CDATA[&]]></xsl:otherwise>
			</xsl:choose>
			<xsl:value-of select="name"/>={<xsl:value-of select="name"/>}
		</xsl:for-each>
	</xsl:template>
	
	<!-- Format data name and value -->
	<xsl:template name="data-name-value">
		<xsl:param name="name"/>
		<xsl:param name="value"/>
		<data name="{$name}"><xsl:value-of select="$value"/></data>
	</xsl:template>
</xsl:stylesheet>
