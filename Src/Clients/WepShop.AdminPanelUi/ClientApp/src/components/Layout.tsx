import * as React from 'react';
import NavMenu from './NavMenu';

export default class Layout extends React.PureComponent<{}, { children?: React.ReactNode }> {
    public render() {
        return (
            <React.Fragment>
                <NavMenu />
                    {this.props.children}
            </React.Fragment>
        );
    }
}