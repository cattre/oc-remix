import { Heading, Box, Container, Table, Stack, Link as NavLink, Button } from '@chakra-ui/react'
import { json, LoaderArgs } from '@remix-run/node'
import { useLoaderData, Link } from '@remix-run/react'
import axios from 'axios'
import { ItemDto, ItemsService } from '~/api/api'
import { requireUserSession } from '~/auth/session.server'
import SimpleTable from '~/components/SimpleTable'

export const loader = async ({ request }: LoaderArgs) => {
  const session = await requireUserSession(request)

  const instance = axios.create({
    headers: { Authorization: `Bearer ${session.data.access_token}` },
    transformResponse: (res) => res,
  })

  const itemsClient = new ItemsService('http://localhost:5986', instance)
  const res = itemsClient.listItems()
  return json(await res)
}

export default function Index() {
  const items: ItemDto[] = useLoaderData<typeof loader>()

  const columns = [
    {id: 'name', label: 'Title'},
    {id: 'description', label: 'Description'},
    {id: 'date', label: 'Due Date'},
    {id: 'isChecked', label: ''}
  ]

  return <Container>
    <Stack direction='row' justifyContent='space-between' alignItems='center'>
      <Heading>Items</Heading>
      <Button as={Link} to='/auth/logout'>Logout</Button>
    </Stack>
    <SimpleTable data={items} columns={columns} />
  </Container>
}
