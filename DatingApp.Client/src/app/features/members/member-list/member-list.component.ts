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
  private memberrService = inject(MembersService);
  members: Member[] = [];

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.memberrService.getMembers().subscribe({
      next: (members) => (this.members = members),
    });
  }
}
