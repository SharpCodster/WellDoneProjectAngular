import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ApplicationPaths } from '../../shared/application.paths';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        RouterModule.forChild(
            [
            // { path: ApplicationPaths.Register, component: LoginComponent },
            // { path: ApplicationPaths.Profile, component: LoginComponent },
            // { path: ApplicationPaths.Login, component: LoginComponent },
            // { path: ApplicationPaths.LoginFailed, component: LoginComponent },
            // { path: ApplicationPaths.LoginCallback, component: LoginComponent },
            // { path: ApplicationPaths.LogOut, component: LogoutComponent },
            // { path: ApplicationPaths.LoggedOut, component: LogoutComponent },
            // { path: ApplicationPaths.LogOutCallback, component: LogoutComponent }
            ]
        )
    ],
    declarations: [
        //LoginMenuComponent, 
        //LoginComponent, 
        //LogoutComponent
    ],
    exports: [
        //LoginMenuComponent, 
        //LoginComponent, 
        //LogoutComponent
    ]
})
export class AuthorizationModule { }