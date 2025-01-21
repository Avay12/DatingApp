import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../../core/services/members.service';
import { Member } from '../../../shared/models/member';
import { MemberCardComponent } from '../member-card/member-card.component';

@Component({
  selector: 'app-member-lists',
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css',
})
export class MemberListComponent implements OnInit {
  memberrService = inject(MembersService);

  ngOnInit(): void {
    if (this.memberrService.members().length === 0) this.loadMembers();
  }

  loadMembers() {
    this.memberrService.getMembers();
  }
}
