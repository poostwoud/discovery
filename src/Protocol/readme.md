Protocol Adapters
-----------------
Converts a protocol response to UBER. For now only HTTP is supported.

This will allow for the generic client to be able to read the parts of the message format that is send via the protocol header (think of profiles in the Link header) in a similar fashion as the "actual" message.


An example using the GitHub API:

```c#
new Protocol.HttpProtocolAdapter().Read(new Uri("https://api.github.com"), "username", "password");
```

Returns:

```xml
<uber version="1.0">
	<data name="response">
		<data name="statusLine" rel="format-documentation" url="http://www.w3.org/Protocols/rfc2616/rfc2616-sec6.html">
			<data name="protocolName">HTTP</data>
			<data name="protocolVersion">1.1</data>
			<data name="statusCode">200</data>
			<data name="statusDescription">OK</data>
		</data>
		<data name="headers">
			<data name="Server">
				<data>GitHub.com</data>
			</data>
			<data name="Date">
				<data>Sun, 21 Sep 2014 23:21:14 GMT</data>
			</data>
			<data name="Content-Type">
				<data>application/json; charset=utf-8</data>
			</data>
			<data name="Status">
				<data>200 OK</data>
			</data>
			<data name="X-RateLimit-Limit">
				<data>5000</data>
			</data>
			<data name="X-RateLimit-Remaining">
				<data>4998</data>
			</data>
			<data name="X-RateLimit-Reset">
				<data>1411345016</data>
			</data>
			<data name="Cache-Control">
				<data>private</data>
				<data>max-age=60</data>
				<data>s-maxage=60</data>
			</data>
			<data name="ETag">
				<data>"5cc97578afe41895a949985a0ae3a870"</data>
			</data>
			<data name="Vary">
				<data>Accept</data>
				<data>Authorization</data>
				<data>Cookie</data>
				<data>X-GitHub-OTP</data>
				<data>Accept-Encoding</data>
			</data>
			<data name="X-GitHub-Media-Type">
				<data>github.v3; format=json</data>
			</data>
			<data name="X-XSS-Protection">
				<data>1; mode=block</data>
			</data>
			<data name="X-Frame-Options">
				<data>deny</data>
			</data>
			<data name="Content-Security-Policy">
				<data>default-src 'none'</data>
			</data>
			<data name="Content-Length">
				<data>1780</data>
			</data>
			<data name="Access-Control-Allow-Credentials">
				<data>true</data>
			</data>
			<data name="Access-Control-Expose-Headers">
				<data>ETag, Link, X-GitHub-OTP, X-RateLimit-Limit, X-RateLimit-Remaining, X-RateLimit-Reset, X-OAuth-Scopes, X-Accepted-OAuth-Scopes, X-Poll-Interval</data>
			</data>
			<data name="Access-Control-Allow-Origin">
				<data>*</data>
			</data>
			<data name="X-GitHub-Request-Id">
				<data>53A3D785:13B0:1674D86B:541F5D69</data>
			</data>
			<data name="Strict-Transport-Security">
				<data>max-age=31536000; includeSubdomains; preload</data>
			</data>
			<data name="X-Content-Type-Options">
				<data>nosniff</data>
			</data>
			<data name="X-Served-By">
				<data>03d91026ad8428f4d9966d7434f9d82e</data>
			</data>
		</data>
		<data name="entity">
			<data name="encoding"></data>
			<data name="type">application/json; charset=utf-8</data>
			<data name="body">{"current_user_url":"https://api.github.com/user","authorizations_url":"https://api.github.com/authorizations","code_search_url":"https://api.github.com/search/code?q={query}{&page,per_page,sort,order}","emails_url":"https://api.github.com/user/emails","emojis_url":"https://api.github.com/emojis","events_url":"https://api.github.com/events","feeds_url":"https://api.github.com/feeds","following_url":"https://api.github.com/user/following{/target}","gists_url":"https://api.github.com/gists{/gist_id}","hub_url":"https://api.github.com/hub","issue_search_url":"https://api.github.com/search/issues?q={query}{&page,per_page,sort,order}","issues_url":"https://api.github.com/issues","keys_url":"https://api.github.com/user/keys","notifications_url":"https://api.github.com/notifications","organization_repositories_url":"https://api.github.com/orgs/{org}/repos{?type,page,per_page,sort}","organization_url":"https://api.github.com/orgs/{org}","public_gists_url":"https://api.github.com/gists/public","rate_limit_url":"https://api.github.com/rate_limit","repository_url":"https://api.github.com/repos/{owner}/{repo}","repository_search_url":"https://api.github.com/search/repositories?q={query}{&page,per_page,sort,order}","current_user_repositories_url":"https://api.github.com/user/repos{?type,page,per_page,sort}","starred_url":"https://api.github.com/user/starred{/owner}{/repo}","starred_gists_url":"https://api.github.com/gists/starred","team_url":"https://api.github.com/teams","user_url":"https://api.github.com/users/{user}","user_organizations_url":"https://api.github.com/user/orgs","user_repositories_url":"https://api.github.com/users/{user}/repos{?type,page,per_page,sort}","user_search_url":"https://api.github.com/search/users?q={query}{&page,per_page,sort,order}"}</data>
			<data name="length">1780</data>
		</data>
	</data>
</uber>
```
