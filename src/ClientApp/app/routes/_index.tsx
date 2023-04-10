import { Heading, Container, HStack, Button } from '@chakra-ui/react'
import { ActionFunction, json, LoaderArgs } from '@remix-run/node'
import { Form, useLoaderData } from '@remix-run/react'
import axios from 'axios'
import { ItemsService } from '~/api/api'
import SimpleTable from '~/components/SimpleTable'
import authenticator from '~/services/auth.server'
import { getSession } from '~/services/session.server'

export const loader = async ({ request }: LoaderArgs) => {
  await authenticator.isAuthenticated(request, {
    failureRedirect: '/auth/login',
  })

  const session = await getSession(
    request.headers.get('Cookie')
  )

  const instance = axios.create({
    headers: { Authorization: `Bearer ${session.data.sessionKey.token}` },
    transformResponse: (res) => res,
  })

  const itemsClient = new ItemsService('http://localhost:5986', instance)
  const res = itemsClient.listItems()
  return json(await res)
}

export const action: ActionFunction = async ({ request }) => {
  await authenticator.logout(request, { redirectTo: `http://localhost:5986/connect/logout` })
}

export default function Index() {
  const items = useLoaderData<typeof loader>()

  const columns = [
    {id: 'name', label: 'Title'},
    {id: 'description', label: 'Description'},
    {id: 'date', label: 'Due Date'},
    {id: 'isChecked', label: ''}
  ]

  return <Container>
    <HStack justifyContent='space-between'>
      <Heading>Items</Heading>
      <Form method='post'>
        <button>Log Out</button>
      </Form>
    </HStack>
    <SimpleTable data={items} columns={columns} />
  </Container>
}
