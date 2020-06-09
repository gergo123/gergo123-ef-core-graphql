import React from 'react';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';

const Tasks = (props) => {
	const itempressed = (itemId) => () => {
		fetch(`${props.apiUrl}api/flowone/FinishTask?taskId=${itemId}`, {
			credentials: 'include',
			method: 'post'
		}).then((resp) => {
			if (resp.ok) {
				props.getTasks();
			} else {
				return Promise.reject(resp);
			}
		}).catch(props.error);
	};

	return (
		<>
			<p>Tasks status</p>
			<List dense>
				{props.tasks.map((item) => {
					return (
						<ListItem key={item.id} button onClick={itempressed(item.id)}>
							<ListItemText id={item.id} primary={`Task (${item.id})`} />
							<ListItemSecondaryAction>
								<Checkbox
									edge="end"
									checked={item.status !== 1}
								/>
							</ListItemSecondaryAction>
						</ListItem>
					);
				})}
			</List>
		</>
	)
};

export default Tasks;
