import { ActionFunction, json } from '@remix-run/node'
import type { LoaderFunction } from '@remix-run/node'
import { Form } from '@remix-run/react'
import authenticator from '~/services/auth.server'
import { getSession } from '~/services/session.server'

export const loader: LoaderFunction = async ({ request, context }) => {
  // await authenticator.isAuthenticated(request, {
  //   successRedirect : '/'
  // })
  //
  // const session = await getSession(
  //   request.headers.get('Cookie')
  // )
  //
  // const error = session.get('sessionErrorKey')
  // return json<any>({ error })

  return await authenticator.isAuthenticated(request, {
    successRedirect: '/',
  })
}

export const action: ActionFunction = async ({ request, context }) => {
  const resp = await authenticator.authenticate('orchard', request, {
    successRedirect: '/',
    failureRedirect: '/auth/login',
    throwOnError: true,
    context,
  })
  console.log(resp)
  return resp
}

export default function Login() {
  return <Form method='post'>
    <button>Sign In</button>
  </Form>
}
