import { json, LoaderArgs } from '@remix-run/node'
import { useLoaderData } from '@remix-run/react'
import axios from 'axios'
import { UsersService } from '~/api/api'
import { requireUserSession } from '~/auth/session.server'

export const loader = async ({ request }: LoaderArgs) => {
  const session = await requireUserSession(request)

  const instance = axios.create({
    headers: { Authorization: `Bearer ${session.data.access_token}` },
    transformResponse: (res) => res,
  })

  const usersClient = new UsersService('http://localhost:5986', instance)
  const res = usersClient.listUsers()
  return json(await res)
}

export default function Users() {
  const users = useLoaderData<typeof loader>()
  return (
    <div>
      <h1>Users</h1>
      {users.map((user) => (
        <div key={user.userId}>{user.userName}</div>
      ))}
    </div>
  )
}
