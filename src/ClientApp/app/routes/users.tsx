import { json } from '@remix-run/node'
import { useLoaderData } from '@remix-run/react'
import axios from 'axios'
import { UsersService } from '~/api/api'

export const loader = async () => {
  const instance = axios.create({
    headers: { Authorization: `Bearer ` },
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
