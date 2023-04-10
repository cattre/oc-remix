import type { LoaderFunction } from "@remix-run/node"
import authenticator from '~/services/auth.server'

export const loader: LoaderFunction = async ({ request, context }) => {
  const resp = await authenticator.authenticate('orchard', request, {
    successRedirect: '/',
    failureRedirect: '/auth/login',
    throwOnError: true,
    context,
  })
  console.log(resp)
  return resp
}

export default function AuthCallback() {
  // todo: show custom messages for auth issues
}
