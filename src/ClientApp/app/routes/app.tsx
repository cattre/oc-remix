import { json, LoaderArgs } from '@remix-run/node'
import { useLoaderData } from '@remix-run/react'
import axios from 'axios/index'
import { UsersService } from '~/api/api'
// import { authenticator } from '~/services/auth.server'
// import { createUserSession, getSession, sessionStorage } from '~/services/session.server'
import { requireUserSession } from '~/auth/session.server'

export const loader = async ({ request }: LoaderArgs) => {
  const session = await requireUserSession(request)
  return json(session)
}

export default function App() {
  const session = useLoaderData<typeof loader>()

  return (
    <div style={{ fontFamily: 'system-ui, sans-serif', lineHeight: '1.4' }}>
      <h1>Welcome {session?.data.user.name}</h1>
    </div>
  )
}
