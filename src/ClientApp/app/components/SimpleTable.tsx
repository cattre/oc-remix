import { Table, TableCaption, TableContainer, Tbody, Td, Tfoot, Th, Thead, Tr } from '@chakra-ui/react'

const getCellData = <T,>(row: T, id: keyof T) => {
  return row[id] as string
}

type Props<T> = {
  data: T[]
  columns: {id: string, label: string}[]
}

export default function SimpleTable<T>(props: Props<T>) {
  return <TableContainer>
    <Table variant='simple'>
      <Thead>
        <Tr>
          {props.columns.map(column => <Th key={column.id}>{column.label}</Th>)}
        </Tr>
      </Thead>
      <Tbody>
        {props.data.map(row => <Tr key={row['id' as keyof T] as string}>
          {props.columns.map(cell => <Td key={cell.id}>{row[cell.id as keyof T] as string}</Td>)}
        </Tr>)}
      </Tbody>
    </Table>
  </TableContainer>
}
