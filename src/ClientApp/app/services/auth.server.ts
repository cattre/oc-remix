import { Authenticator } from 'remix-auth'
import { OAuth2Strategy } from 'remix-auth-oauth2'
import { sessionStorage, User } from '~/services/session.server'

// Create an instance of the authenticator, pass a generic with what
// strategies will return and will store in the session
// Create an instance of the authenticator, pass a Type, User,  with what
// strategies will return and will store in the session
const authenticator = new Authenticator<User | Error | null>(sessionStorage, {
  sessionKey: "sessionKey", // keep in sync
  sessionErrorKey: "sessionErrorKey", // keep in sync
})

authenticator.use(
  new OAuth2Strategy(
    {
      authorizationURL: 'http://localhost:5986/connect/authorize',
      tokenURL: 'http://localhost:5986/connect/token',
      clientID: 'ClientApp',
      clientSecret: 'secret',
      callbackURL: 'http://localhost:3000/auth/callback',
      useBasicAuthenticationHeader: false // defaults to false
    },
    async ({ accessToken, refreshToken, extraParams, profile, context }) => {
      // here you can use the params above to get the user and return it
      // what you do inside this and how you find the user is up to you

      console.log(accessToken, refreshToken, extraParams, profile, context)
      return {
        name: profile.displayName ?? '',
        token: accessToken
      }
    }
  ),
  // this is optional, but if you setup more than one OAuth2 instance you will
  // need to set a custom name to each one
  'orchard'
)

export default authenticator
